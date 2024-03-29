﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    internal class PackageContributionRegistry : IContributionRegistry
    {
        private readonly ContributionRegistry _registry;
        private readonly IServiceProvider _serviceProvider;

        public PackageContributionRegistry(
            ContributionRegistry registry,
            IServiceProvider serviceProvider)
        {
            _registry = registry;

            _serviceProvider = serviceProvider;
        }

        public IContributionHost Get(string rootId)
        {
            return _registry.Get(rootId);
        }

        public IContributionHost Add(IContributionHost host)
        {
            return _registry.Add(host);
        }

        public bool Remove(IContributionHost host)
        {
            return _registry.Remove(host);
        }

        public void Register<TContribution>(
            string rootId, string? parentId = null) where TContribution : IContribution
        {
            _registry.Register<TContribution>(_serviceProvider, rootId, parentId);
        }

        public bool Unregister<TContribution>(
            string rootId) where TContribution : IContribution
        {
            return _registry.Unregister<TContribution>(_serviceProvider, rootId);
        }
    }
}
