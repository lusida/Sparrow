using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Extensions.Main
{
    [Injection<IContributionHost>]
    internal class AppMenuContributionHost : ContributionHost
    {
        public AppMenuContributionHost() : base(ContributionIds.APP_MENU_ID)
        {
        }

        public override void Register<TContribution>(
            TContribution contribution, string? parentId = null)
        {
            throw new NotImplementedException();
        }

        public override bool Unregister<TContribution>(TContribution contribution)
        {
            throw new NotImplementedException();
        }
    }
}
