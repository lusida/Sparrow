using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public interface IAppEnvironment
    {
        string Title { get; }

        string BaseDirectory { get; }
        string BuiltDirectory { get; }
        string ExtensionDirectory { get; }
        string UserProfileDirectory { get; }
        string AppDataDirectory { get; }
        string AppDirectoryName { get; }
    }
}
