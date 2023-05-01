using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PackageAttribute : InjectionAttribute<IPackage>
    {

    }
}
