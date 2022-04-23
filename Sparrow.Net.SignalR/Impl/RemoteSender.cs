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
    internal class RemoteSender : IRemoteSender, IDisposable
    {
        private readonly HubConnection _connection;
        private CancellationTokenSource? _cancellationTokenSource;

        public RemoteSender(string hostUrl)
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

        private CancellationToken GetCancellationToken(CancellationToken? cancellationToken)
        {
            if (_cancellationTokenSource == null ||
                _cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource = new CancellationTokenSource();
            }

            if (cancellationToken.HasValue)
            {
                var source = CancellationTokenSource.CreateLinkedTokenSource(
                    _cancellationTokenSource.Token, cancellationToken.Value);

                return source.Token;
            }
            else
            {
                return _cancellationTokenSource.Token;
            }
        }

        public async void Execute(RemoteCall call)
        {
            var cancellicationToken = this.GetCancellationToken(null);

            await _connection.SendAsync(
                "Execute", call, cancellicationToken);
        }

        public Task ExecuteAsync(RemoteCall call, CancellationToken cancellationToken)
        {
            var cancellicationToken = this.GetCancellationToken(cancellationToken);

            return _connection.SendAsync(
                "Execute", call, cancellicationToken);
        }

        public object Invoke(RemoteCall call)
        {
            var cancellicationToken = this.GetCancellationToken(null);

            var task = _connection.InvokeAsync<RemoteResult>(
                "Invoke", call, cancellicationToken);

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

        public async Task<object> InvokeAsync(RemoteCall call, CancellationToken cancellationToken)
        {
            var cancellicationToken = this.GetCancellationToken(cancellationToken);

            var r = await _connection.InvokeAsync<RemoteResult>(
                "Invoke", call, cancellicationToken);

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
