using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public abstract class Package : IPackage
    {
        protected readonly IPackageContext _context;

        public Package(IPackageContext context)
        {
            _context = context;
        }

        public bool IsLoaded { get; private set; }

        async Task IPackage.LoadAsync(CancellationToken cancellationToken)
        {
            if (!this.IsLoaded)
            {
                await this.OnLoadAsync(cancellationToken);

                this.IsLoaded = true;

                ((PackageContext)_context).IsLoaded = true;
            }
        }

        async Task IPackage.UnloadAsync(CancellationToken cancellationToken)
        {
            if (this.IsLoaded)
            {
                await this.OnUnloadAsync(cancellationToken);

                this.IsLoaded = false;

                ((PackageContext)_context).IsLoaded = false;
            }
        }

        protected virtual Task OnLoadAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnUnloadAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
