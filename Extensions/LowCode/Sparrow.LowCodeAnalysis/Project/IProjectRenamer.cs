using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public interface IProjectRenamer
    {
        int Order { get; }
        Task RenameAsync(
            Project oldProjec, Project newProjec, CancellationToken cancellationToken);
    }
}
