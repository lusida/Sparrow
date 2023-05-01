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

        public bool Unregister<TContribution>() where TContribution : IContribution
        {
            throw new NotImplementedException();
        }

        public void Register<TContribution>(
            IServiceProvider serviceProvider,
            string rootId,
            string? parentId = null)
            where TContribution : IContribution
        {
            throw new NotImplementedException();
        }
    }
}
