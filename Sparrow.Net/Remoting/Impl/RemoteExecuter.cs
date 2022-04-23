using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;

namespace Sparrow.Net.Remoting
{
    internal class RemoteExecuter : IRemoteExecuter
    {
        private readonly IServiceProvider _provider;
        private readonly IInterceptor[] _interceptors;
        private readonly ConcurrentDictionary<string, MethodInfo?> _methods;

        public RemoteExecuter(IServiceProvider provider)
        {
            _provider = provider;

            _interceptors = new IInterceptor[]
            {
                new RemoteInterceptor()
            };

            _methods = new ConcurrentDictionary<string, MethodInfo?>();

            this.Callback = _provider.GetRequiredService<IRemoteCallback>();
        }

        public IRemoteCallback Callback { get; }

        public async Task<RemoteResult> ExecuteAsync(
            RemoteCall call, CancellationToken cancellationToken)
        {
            if (call.IsCallback)
            {
                await this.Callback.CallbackAsync(call, cancellationToken);

                return RemoteResult.Void;
            }
            else
            {
                var message = "";

                var type = Type.GetType(call.TypeName);

                if (type != null)
                {
                    var obj = _provider.GetService(type);

                    if (obj != null)
                    {
                        var method = _methods.GetOrAdd(
                            $"{call.TypeName}/{call.MethodName}",
                            k => type.GetMethod(call.MethodName));

                        if (method != null)
                        {
                            var args = new List<object>();

                            var m = new RemoteInvocation(
                                obj, method, _interceptors, args.ToArray());

                            m.Proceed();

                            if (m.ReturnValue == null)
                            {
                                return new RemoteResult(
                                    method.ReturnType.AssemblyQualifiedName ?? String.Empty, String.Empty, true, message);
                            }
                            else
                            {
                                var returnType = m.ReturnValue.GetType();

                                var value = JsonSerializer.Serialize(m.ReturnValue, returnType);

                                return new RemoteResult(
                                    returnType.AssemblyQualifiedName ?? returnType.Name, value, true, message); ;
                            }
                        }
                    }
                }

                return RemoteResult.Error(message);
            }
        }
    }
}
