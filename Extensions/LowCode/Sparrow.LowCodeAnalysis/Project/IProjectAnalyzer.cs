using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public interface IProjectAnalyzer
    {
        int Order { get; }
        Task AnalyzeAsync(
            Project project, CancellationToken cancellationToken);
    }
}
