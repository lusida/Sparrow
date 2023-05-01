using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Common
{
    public interface IDetectionOptions
    {
        string Directory { get; set; }
        IServiceCollection Services { get; }
        AssemblyLoadContext AssemblyLoadContext { get; set; }
        IDictionary<Type, IDetectionProvider> Providers { get; }
    }
}
