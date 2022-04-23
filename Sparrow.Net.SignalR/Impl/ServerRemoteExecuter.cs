using Microsoft.AspNetCore.SignalR;
using Sparrow.Net.Remoting;

namespace Sparrow.Net.SignalR
{
    [RemoteService<IRemoteExecuter>]
    internal class ServerRemoteExecuter : IRemoteExecuter
    {
        public Hub? Hub { get; set; }
        public Action<RemoteCall>? Callback { get; set; }

        public void Execute(RemoteCall method)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteAsync(RemoteCall method)
        {
            throw new NotImplementedException();
        }

        public object Invoke(RemoteCall method)
        {
            throw new NotImplementedException();
        }

        public Task<object> InvokeAsync(RemoteCall method)
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}
