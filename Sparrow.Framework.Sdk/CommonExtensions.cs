using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public static class CommonExtensions
    {
        public static TContributionHost Get<TContributionHost>(
            this IContributionRegistry registry, string rootId) where TContributionHost : IContributionHost
        {
            var c = registry.Get(rootId);

            return (TContributionHost)c;
        }
    }
}
