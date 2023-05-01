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
        private readonly ConcurrentDictionary<Type, IContributionHost> _hosts;

        public ContributionRegistry(IEnumerable<IContributionHost> hosts)
        {
            _hosts = new ConcurrentDictionary<Type, IContributionHost>(
                hosts.ToDictionary(m => m.AttributeType));
        }

        public IContributionHost Add(IContributionHost host)
        {
            return _hosts.GetOrAdd(host.AttributeType, host);
        }

        public bool Remove(IContributionHost host)
        {
            return _hosts.TryRemove(host.AttributeType, out var _);
        }

        public void Register<TContribution>() where TContribution : IContribution
        {
            throw new NotImplementedException();
        }

        public bool Unregister<TContribution>() where TContribution : IContribution
        {
            throw new NotImplementedException();
        }

        public void Register<TContribution>(
            IServiceProvider serviceProvider) where TContribution : IContribution
        {
            throw new NotImplementedException();
        }

        public bool Unregister<TContribution>(
            IServiceProvider serviceProvider) where TContribution : IContribution
        {
            throw new NotImplementedException();
        }
    }
}
