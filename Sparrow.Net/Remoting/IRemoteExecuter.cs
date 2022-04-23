using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    public interface IRemoteExecuter
    {
        IRemoteCallback Callback { get; }
        Task<RemoteResult> ExecuteAsync(
            RemoteCall call, CancellationToken cancellationToken);
    }
}
