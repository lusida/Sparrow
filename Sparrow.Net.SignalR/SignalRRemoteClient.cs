using Sparrow.Net.Remoting;

namespace Sparrow.Net.SignalR
{
    public class SignalRRemoteClient : RemoteClient
    {
        public SignalRRemoteClient(string hostUrl) 
            : base(new RemoteSender(hostUrl))
        {
        }
    }
}