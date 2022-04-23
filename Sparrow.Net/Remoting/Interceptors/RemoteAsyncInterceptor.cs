using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    internal class RemoteAsyncInterceptor : IAsyncInterceptor
    {
        private readonly IRemoteExecuter _executer;

        public RemoteAsyncInterceptor(IRemoteExecuter executer)
        {
            _executer = executer;
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            throw new NotImplementedException();
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            throw new NotImplementedException();
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
