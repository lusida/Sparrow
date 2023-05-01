using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public abstract class ContributionHost : IContributionHost
    {
        protected ContributionHost(string rootId)
        {
            this.RootId = rootId;
        }

        public string RootId { get; }

        public abstract void Register<TContribution>(
            TContribution contribution, string? parentId = null) where TContribution : IContribution;

        public abstract bool Unregister<TContribution>(
            TContribution contribution) where TContribution : IContribution;
    }
}
