using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    internal class RemoteAsyncInterceptor : IAsyncInterceptor
    {
        private readonly Delegate _func;
        public RemoteAsyncInterceptor()
        {
            var method = typeof(RemoteAsyncInterceptor).GetMethod("GetResult")!;

            var arg = Expression.Parameter(typeof(Task<>));

            var call = Expression.Call(method, arg);

            var lambda = Expression.Lambda(call, arg);

            _func = lambda.Compile();
        }

        public static TResult GetResult<TResult>(Task<TResult> task)
        {
            return task.GetAwaiter().GetResult();
        }

        private async void Invoke(IInvocation invocation)
        {
            var obj = invocation.Method.Invoke(
                invocation.Proxy, invocation.Arguments);

            if (obj is Task task)
            {
                if (invocation.Method.ReturnType.IsGenericType)
                {
                    invocation.ReturnValue = _func.DynamicInvoke(obj);
                }
                else
                {
                    await task;
                }
            }
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            this.Invoke(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            this.Invoke(invocation);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            this.Invoke(invocation);
        }
    }
}
