using Microsoft.AspNetCore.SignalR;
using Sparrow.Net.Remoting;

namespace Sparrow.Net.SignalR
{
    [RemoteService<IRemoteCallback>]
    internal class RemoteCallback : IRemoteCallback
    {
        public Hub? Hub { get; set; }

        public async Task<RemoteResult> CallbackAsync(
            RemoteCall call, CancellationToken cancellationToken)
        {
            if (this.Hub != null)
            {
                if (call.IsCallback)
                {
                    await this.Hub.Clients.Others.SendAsync("Receive", call,cancellationToken);
                }
                else
                {
                    await this.Hub.Clients.All.SendAsync("Receive", call,cancellationToken);
                }
            }

            return RemoteResult.Void;
        }
    }
}
