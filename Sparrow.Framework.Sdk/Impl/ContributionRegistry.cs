using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    internal class ContributionRegistry : IContributionRegistry
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<string, IContributionHost> _hosts;

        public ContributionRegistry(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var items = serviceProvider.GetServices<IContributionHost>();

            _hosts = new ConcurrentDictionary<string, IContributionHost>(
                items.ToDictionary(m => m.RootId));
        }

        public IContributionHost Get(string rootId)
        {
            if (_hosts.TryGetValue(rootId, out var host))
            {
                return host;
            }

            throw new KeyNotFoundException(rootId);
        }

        public IContributionHost Add(IContributionHost host)
        {
            return _hosts.GetOrAdd(host.RootId, host);
        }

        public bool Remove(IContributionHost host)
        {
            return _hosts.TryRemove(host.RootId, out var _);
        }

        public void Register<TContribution>(
            string rootId, string? parentId = null) where TContribution : IContribution
        {
            this.Register<TContribution>(_serviceProvider, rootId, parentId);
        }

        public bool Unregister<TContribution>(
            string rootId) where TContribution : IContribution
        {
            return this.Unregister<TContribution>(_serviceProvider, rootId);
        }

        public bool Unregister<TContribution>(
            IServiceProvider serviceProvider, string rootId) where TContribution : IContribution
        {
            if (_hosts.TryGetValue(rootId, out var host))
            {
                var contribution =
                    serviceProvider.GetRequiredService<TContribution>();

                return host.Unregister(contribution);
            }

            return false;
        }

        public void Register<TContribution>(
            IServiceProvider serviceProvider,
            string rootId,
            string? parentId = null)
            where TContribution : IContribution
        {
            if (_hosts.TryGetValue(rootId, out var host))
            {
                var contribution =
                    serviceProvider.GetRequiredService<TContribution>();

                host.Register(contribution, parentId);
            }
        }
    }
}
