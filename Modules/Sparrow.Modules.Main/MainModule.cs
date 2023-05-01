using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Modules.Main
{
    [AppModule]
    internal class MainModule : AppModule
    {
        public MainModule(IContributionRegistry registry) : base(registry)
        {

        }

        public override void Initialize()
        {
            _registry.Register<FileContribution>(ContributionIds.APP_MENU_ID);
        }
    }
}
