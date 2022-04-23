using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Sparrow.Net.Remoting
{
    public abstract class RemoteClient : IDisposable
    {
        private readonly ProxyGenerator _generator = new ProxyGenerator();
        private readonly ProxyGenerationOptions _options = new ProxyGenerationOptions();

        protected RemoteClient(IRemoteExecuter executer)
        {
            this.Executer = executer;

            _options.Selector = new RemoteInterceptorSelector(this.Executer);
        }

        public IRemoteExecuter Executer { get; }

        public TService GetService<TService>() where TService : class
        {
            return _generator.CreateInterfaceProxyWithoutTarget<TService>(_options);
        }

        public virtual void Dispose()
        {
            this.Executer.Cancel();

            if (this.Executer is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
