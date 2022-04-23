using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    public interface IRemoteCallback
    {
        Task<RemoteResult> CallbackAsync(
            RemoteCall call, CancellationToken cancellationToken);
    }
}
