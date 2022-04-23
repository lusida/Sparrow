using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Sparrow.Net.Remoting
{
    internal class RemoteInterceptorSelector : IInterceptorSelector
    {
        private readonly IInterceptor[] _interceptors;

        public RemoteInterceptorSelector(IRemoteExecuter executer)
        {
            _interceptors = new IInterceptor[]
            {
                new RemoteInterceptor(executer)
            };
        }

        public IInterceptor[] SelectInterceptors(
            Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            return _interceptors;
        }
    }
}
