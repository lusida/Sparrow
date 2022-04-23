using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Sparrow.Net.Remoting
{
    internal class ClientInterceptor : IInterceptor
    {
        private readonly Type _taskType=typeof(Task);
        private readonly IRemoteSender _executer;
        private readonly IAsyncInterceptor _intercptor;

        public ClientInterceptor(IRemoteSender executer)
        {
            _executer = executer;

            _intercptor = new ClientAsyncInterceptor(_executer);
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.ReturnType.IsSubclassOf(_taskType))
            {
                var a = _intercptor.ToInterceptor();

                a.Intercept(invocation);
            }
            else
            {

            }
        }
    }
}
