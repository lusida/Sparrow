using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    internal class ClientAsyncInterceptor : IAsyncInterceptor
    {
        private readonly IRemoteSender _sender;

        public ClientAsyncInterceptor(IRemoteSender sender)
        {
            _sender = sender;
        }

        private RemoteCall CreateCall(IInvocation invocation, out CancellationToken cancellationToken)
        {
            cancellationToken = CancellationToken.None;

            var args = new Dictionary<string, RemoteResult>();

            foreach (var p in invocation.Method.GetParameters())
            {
                var pobj = invocation.GetArgumentValue(p.Position);

                if (pobj is CancellationToken c)
                {
                    cancellationToken = c;
                }
                else
                {
                    var v = JsonSerializer.Serialize(pobj);

                    args.Add(
                        p.Name!,
                        new RemoteResult(p.ParameterType.AssemblyQualifiedName!, v, true, String.Empty));
                }
            }

            return new RemoteCall(
                false,
                invocation.Method.DeclaringType!.AssemblyQualifiedName!,
                invocation.Method.Name,
                args);
        }

        public async void InterceptAsynchronous(IInvocation invocation)
        {
            var call = this.CreateCall(
                invocation, out var cancellationToken);

            invocation.ReturnValue =
                await _sender.InvokeAsync(call, cancellationToken);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            this.InterceptAsynchronous(invocation);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            var call = this.CreateCall(
                invocation, out var cancellationToken);

            invocation.ReturnValue = _sender.Invoke(call);
        }
    }
}
