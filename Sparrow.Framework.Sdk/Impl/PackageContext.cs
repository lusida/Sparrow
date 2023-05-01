using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    internal class PackageContext : IPackageContext, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceProvider _hostServiceProvider;
        private readonly AssemblyLoadContext _loadContext;

        public PackageContext(
            IServiceProvider hostServiceProvider,
            PackageMetadata metadata)
        {
            _hostServiceProvider = hostServiceProvider;

            _loadContext = new AssemblyLoadContext(metadata.Id, true);

            this.Metadata = metadata;

            var services = new ServiceCollection();

            services.AddSingleton<IPackageContext>(this);

            services.AddSingleton(
                p => _hostServiceProvider.GetRequiredService<ContributionRegistry>());

            services.AddSingleton<IContributionRegistry, PackageContributionRegistry>();

            services.AddInjections(options =>
            {
                options.AssemblyLoadContext = _loadContext;

                options.Directory = metadata.DirectoryPath;

            });

            _serviceProvider = services.BuildServiceProvider();
        }

        public bool IsLoaded { get; set; }

        public PackageMetadata Metadata { get; }

        public object? GetService(Type serviceType)
        {
            var obj = _serviceProvider.GetService(serviceType);

            obj ??= _hostServiceProvider.GetService(serviceType);

            return obj;
        }

        public void Dispose()
        {
            _loadContext.Unload();
        }
    }
}
