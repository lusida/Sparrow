using Microsoft.AspNetCore.SignalR;
using Sparrow.Net.Remoting;

namespace Sparrow.Net.SignalR
{
    public class ServerRemoteHub : Hub
    {
        private readonly IRemoteExecuter _executer;
        private readonly IServiceProvider _provider;
        public ServerRemoteHub(
            IRemoteExecuter executer, IServiceProvider provider)
        {
            _executer = executer;

            _provider = provider;

            if (_executer is ServerRemoteExecuter remoteExecuter)
            {
                remoteExecuter.Hub = this;
            }
        }

        public Task Execute(RemoteCall call)
        {
            return this.Invoke(call);
        }

        public Task<RemoteResult> Invoke(RemoteCall call)
        {
            var type = Type.GetType(call.TypeName);

            if (type != null)
            {
                var obj = _provider.GetService(type);

                if (obj != null)
                {

                }
            }
        }
    }
}
