using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Sparrow.Net.Remoting
{
    internal class RemoteInterceptor : IInterceptor
    {
        private readonly IRemoteExecuter _executer;
        private readonly IAsyncInterceptor _intercptor;

        public RemoteInterceptor(IRemoteExecuter executer)
        {
            _executer = executer;

            _intercptor = new RemoteAsyncInterceptor(_executer);
        }

        public void Intercept(IInvocation invocation)
        {

        }
    }
}
