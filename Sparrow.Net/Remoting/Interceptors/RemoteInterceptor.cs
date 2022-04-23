using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    internal class RemoteInterceptor : IInterceptor
    {
        private readonly Type _taskType = typeof(Task);
        private readonly IInterceptor _intrcptor;

        public RemoteInterceptor()
        {
            _intrcptor = new RemoteAsyncInterceptor().ToInterceptor();
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.ReturnType.IsSubclassOf(_taskType))
            {
                _intrcptor.Intercept(invocation);
            }
            else
            {
                invocation.ReturnValue = 
                    invocation.Method.Invoke(invocation.Proxy, invocation.Arguments);
            }
        }
    }
}
