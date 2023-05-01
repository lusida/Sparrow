using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public abstract class ContributionHost<TAttribute>
        : IContributionHost where TAttribute : InjectionAttribute
    {
        protected ContributionHost()
        {
            this.AttributeType = typeof(TAttribute);
        }

        public Type AttributeType { get; }
    }
}
