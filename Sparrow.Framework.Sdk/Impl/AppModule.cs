using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public abstract class AppModule : IAppModule
    {
        protected readonly IContributionRegistry _registry;
        protected AppModule(IContributionRegistry registry)
        {
            _registry = registry;
        }

        public int Order { get; init; }

        public abstract void Initialize();
    }
}
