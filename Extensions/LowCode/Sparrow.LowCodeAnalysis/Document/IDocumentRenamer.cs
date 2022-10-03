using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public interface IDocumentRenamer : IDocumentService
    {
        Task RenameAsync(
            Document oldDocument, Document newDocument, CancellationToken cancellationToken);
    }
}
