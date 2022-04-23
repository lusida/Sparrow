using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace Sparrow.Net.Remoting
{
    public abstract class RemoteServer : IServiceProvider
    {
        private readonly IServiceProvider _provider;
        private readonly ProxyGenerator _generator = new ProxyGenerator();
        private readonly ProxyGenerationOptions _options = new ProxyGenerationOptions();

        protected RemoteServer(string hostUrl, string[] args)
        {
            this.HostUrl = hostUrl;

            this.Services = this.Initialize(args);

            this.WithAssembly(this.GetType().Assembly);

            _provider = this.Services.BuildServiceProvider();

            var executer = _provider.GetRequiredService<IRemoteSender>();

            _options.Selector = new ClientInterceptorSelector(executer);
        }

        public string HostUrl { get; }

        public IServiceCollection Services { get; }

        private bool IsService(
            Type type, out RemoteServiceAttribute? attribute)
        {
            attribute = null;

            if (type.IsClass && !type.IsAbstract)
            {
                attribute = type.GetCustomAttribute<RemoteServiceAttribute>();
            }

            return attribute != null;
        }

        private bool IsCallback(
            Type type, out RemoteCallbackAttribute? attribute)
        {
            attribute = null;

            if (type.IsInterface)
            {
                attribute = type.GetCustomAttribute<RemoteCallbackAttribute>();
            }

            return attribute != null;
        }

        public virtual void WithAssembly(Assembly assembly)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                if (this.IsCallback(type, out var rcattr) && rcattr != null)
                {
                    this.Services.AddSingleton(
                        type,
                        k => _generator.CreateInterfaceProxyWithoutTarget(type, _options));
                }
                else if (this.IsService(type, out var rsattr) && rsattr != null)
                {
                    var serviceType = rsattr.ServiceType ?? type;

                    switch (rsattr.Lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            {
                                this.Services.AddSingleton(serviceType, type);
                            }
                            break;
                        case ServiceLifetime.Scoped:
                            {
                                this.Services.AddScoped(serviceType, type);
                            }
                            break;
                        case ServiceLifetime.Transient:
                            {
                                this.Services.AddTransient(serviceType, type);
                            }
                            break;
                    }
                }
            }
        }

        public object? GetService(Type serviceType)
        {
            return _provider.GetService(serviceType);
        }

        protected abstract IServiceCollection Initialize(string[] args);
        public abstract void Run();
    }
}
