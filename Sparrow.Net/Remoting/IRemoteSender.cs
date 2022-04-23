using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    public interface IRemoteSender
    {
        Action<RemoteCall>? Callback { get; set; }

        void Execute(RemoteCall call);
        Task ExecuteAsync(RemoteCall call, CancellationToken cancellationToken);
        object Invoke(RemoteCall call);
        Task<object> InvokeAsync(RemoteCall call, CancellationToken cancellationToken);

        void Cancel();
    }
}
