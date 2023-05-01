using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    internal class PackageManager : IPackageManager
    {
        private readonly IAppEnvironment _environment;
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<string, IPackageContext> _items;

        public PackageManager(
            IAppEnvironment environment,
            IServiceProvider serviceProvider)
        {
            _environment = environment;

            _serviceProvider = serviceProvider;

            _items = new ConcurrentDictionary<string, IPackageContext>(StringComparer.OrdinalIgnoreCase);
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            await this.LoadAsync(_environment.BuiltDirectory, cancellationToken);

            await this.LoadAsync(_environment.ExtensionDirectory, cancellationToken);
        }

        private async Task LoadAsync(string directory, CancellationToken cancellationToken)
        {
            var spxFiles = Directory.GetFiles(
                directory, "*.spx", SearchOption.AllDirectories);

            foreach (var spxFile in spxFiles)
            {
                await this.LoadPackageAsync(spxFile, cancellationToken);
            }
        }

        private async Task<bool> LoadPackageAsync(string spxFile, CancellationToken cancellationToken)
        {
            var json = await File.ReadAllTextAsync(spxFile, cancellationToken);

            var metadata = JsonSerializer.Deserialize<PackageMetadata>(json);

            if (metadata != null)
            {
                metadata.FilePath = spxFile;

                metadata.DirectoryPath = Path.GetDirectoryName(spxFile)!;


                var context = _items.GetOrAdd(
                    metadata.Id,
                    k => new PackageContext(_serviceProvider, metadata));

                var package = context.GetService<IPackage>();

                if (package == null)
                {
                    if (_items.TryRemove(metadata.Id, out var ctx) &&
                        ctx is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
                else
                {
                    await package.LoadAsync(cancellationToken);

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> InstallAsync(string packageFile, CancellationToken cancellationToken)
        {
            var targetDir = Path.Combine(
                _environment.ExtensionDirectory, Guid.NewGuid().ToString());

            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            ZipFile.ExtractToDirectory(packageFile, targetDir);

            var spxFile = Path.Combine(targetDir, "package.spx");

            if (File.Exists(spxFile))
            {
                return await this.LoadPackageAsync(spxFile, cancellationToken);
            }
            else
            {
                Directory.Delete(targetDir);
            }

            return false;
        }

        public async Task<bool> UninstallAsync(string packageId, CancellationToken cancellationToken)
        {
            if (_items.TryRemove(packageId, out var context))
            {
                var package = context.GetRequiredService<IPackage>();

                await package.UnloadAsync(cancellationToken);

                return true;
            }

            return false;
        }
    }
}
