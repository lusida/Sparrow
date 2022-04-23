using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    internal class RemoteInvocation : AbstractInvocation
    {
        public RemoteInvocation(
            object proxy, MethodInfo proxiedMethod, IInterceptor[] interceptors, object[] arguments)
            : base(proxy, interceptors, proxiedMethod, arguments)
        {

        }

        public override object InvocationTarget => base.Proxy;

        public override Type TargetType => base.Proxy.GetType();

        public override MethodInfo MethodInvocationTarget => base.Method;

        protected override void InvokeMethodOnTarget()
        {
            throw new NotImplementedException();
        }
    }
}
