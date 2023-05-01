using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public interface IAppModule
    {
        int Order { get; }

        void Initialize();
    }
}
