using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public interface IDocumentAnalyzer : IDocumentService
    {
        Task AnalyzeAsync(
            Document document, CancellationToken cancellationToken);
    }
}
