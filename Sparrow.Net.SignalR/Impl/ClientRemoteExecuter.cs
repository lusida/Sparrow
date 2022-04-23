using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Sparrow.Net.Remoting;

namespace Sparrow.Net.SignalR
{
    internal class ClientRemoteExecuter : IRemoteExecuter, IDisposable
    {
        private readonly HubConnection _connection;
        private CancellationTokenSource? _cancellationTokenSource;

        public ClientRemoteExecuter(string hostUrl)
        {
            var builder = new HubConnectionBuilder().WithUrl(hostUrl);

            _connection = builder.Build();

            _connection.On<RemoteCall>("Receive", this.OnCallback);
        }

        public Action<RemoteCall>? Callback { get; set; }

        private void OnCallback(RemoteCall call)
        {
            this.Callback?.Invoke(call);
        }

        private CancellationToken GetCancellationToken()
        {
            if (_cancellationTokenSource == null ||
                _cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource = new CancellationTokenSource();
            }

            return _cancellationTokenSource.Token;
        }

        public async void Execute(RemoteCall method)
        {
            var cancellicationToken = this.GetCancellationToken();

            await _connection.SendAsync(
                "Execute", method, cancellicationToken);
        }

        public Task ExecuteAsync(RemoteCall method)
        {
            var cancellicationToken = this.GetCancellationToken();

            return _connection.SendAsync(
                "Execute", method, cancellicationToken);
        }

        public object Invoke(RemoteCall method)
        {
            var cancellicationToken = this.GetCancellationToken();

            var task = _connection.InvokeAsync<RemoteResult>(
                "Invoke", method, cancellicationToken);

            var r = task.GetAwaiter().GetResult();

            if (r != null)
            {
                var type = Type.GetType(r.TypeName);

                if (type != null)
                {
                    return JsonSerializer.Deserialize(r.Value, type);
                }
            }

            return null;
        }

        public async Task<object> InvokeAsync(RemoteCall method)
        {
            var cancellicationToken = this.GetCancellationToken();

            var r = await _connection.InvokeAsync<RemoteResult>(
                "Invoke", method, cancellicationToken);

            if (r != null)
            {
                var type = Type.GetType(r.TypeName);

                if (type != null)
                {
                    return JsonSerializer.Deserialize(r.Value, type);
                }
            }

            return null;
        }

        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
        }

        public async void Dispose()
        {
            this.Cancel();

            await _connection.DisposeAsync();
        }
    }
}
