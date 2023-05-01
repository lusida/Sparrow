using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public interface IPackage
    {
        bool IsLoaded { get; }
        Task LoadAsync(CancellationToken cancellationToken);
        Task UnloadAsync(CancellationToken cancellationToken);
    }
}
