using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Sparrow.Net.Remoting
{
    internal class ClientInterceptorSelector : IInterceptorSelector
    {
        private readonly IInterceptor[] _interceptors;

        public ClientInterceptorSelector(IRemoteSender executer)
        {
            _interceptors = new IInterceptor[]
            {
                new ClientInterceptor(executer)
            };
        }

        public IInterceptor[] SelectInterceptors(
            Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            return _interceptors;
        }
    }
}
