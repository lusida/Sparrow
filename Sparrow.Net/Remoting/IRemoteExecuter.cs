using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    public interface IRemoteExecuter
    {
        Action<RemoteCall>? Callback { get; set; }

        void Execute(RemoteCall method);
        object Invoke(RemoteCall method);

        Task ExecuteAsync(RemoteCall method);
        Task<object> InvokeAsync(RemoteCall method);

        void Cancel();
    }
}
