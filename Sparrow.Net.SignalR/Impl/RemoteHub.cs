using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Sparrow.Net.Remoting;

namespace Sparrow.Net.SignalR
{
    public class RemoteHub : Hub
    {
        private readonly IRemoteExecuter _executer;
        public RemoteHub(IRemoteExecuter executer)
        {
            _executer = executer;

            if (_executer.Callback is RemoteCallback c)
            {
                c.Hub = this;
            }
        }

        public Task Execute(RemoteCall call)
        {
            return this.Invoke(call);
        }

        public async Task<RemoteResult> Invoke(RemoteCall call)
        {
            return await _executer.ExecuteAsync(call, CancellationToken.None);
        }
    }
}
