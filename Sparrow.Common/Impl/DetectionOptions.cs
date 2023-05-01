using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Common
{
    internal class DetectionOptions : IDetectionOptions
    {
        public DetectionOptions(IServiceCollection services)
        {
            this.Directory = AppContext.BaseDirectory;

            this.AssemblyLoadContext = AssemblyLoadContext.Default;

            this.Providers = new Dictionary<Type, IDetectionProvider>();

            this.Services = services;
        }

        public string Directory { get; set; }

        public IServiceCollection Services { get; }

        public AssemblyLoadContext AssemblyLoadContext { get; set; }

        public IDictionary<Type, IDetectionProvider> Providers { get; }
    }
}
