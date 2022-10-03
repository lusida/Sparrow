using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    [WorkspaceService<IDocumentServiceFactory>]
    internal class DocumentServiceFactory : IDocumentServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<Type, IDictionary<string, IDocumentService[]>> _items;

        public DocumentServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _items = new ConcurrentDictionary<Type, IDictionary<string, IDocumentService[]>>();
        }

        public TService GetRequiredService<TService>(
            string extension) where TService : IDocumentService
        {
            var items = this.GetServices<TService>(extension);

            return items.First();
        }

        public TService? GetService<TService>(
            string extension) where TService : IDocumentService
        {
            var items = this.GetServices<TService>(extension);

            return items.FirstOrDefault();
        }

        public IEnumerable<TService> GetServices<TService>(
            string extension) where TService : IDocumentService
        {
            var items = this.GetOrAdd<TService>();

            if (items.TryGetValue(extension, out var services))
            {
                return services.Cast<TService>();
            }

            return Array.Empty<TService>();
        }

        private IDictionary<string, IDocumentService[]> GetOrAdd<TService>() where TService : IDocumentService
        {
            return _items.GetOrAdd(
                typeof(TService),
                k =>
                {
                    var services = _serviceProvider.GetServices<TService>();

                    return services.GroupBy(
                        m => m.Extension).ToDictionary(
                        m => m.Key, m => m.OrderBy(m => m.Order).OfType<IDocumentService>().ToArray());
                });
        }
    }
}
