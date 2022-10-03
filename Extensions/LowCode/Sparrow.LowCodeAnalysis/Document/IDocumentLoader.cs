using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public interface IDocumentLoader : IDocumentService
    {
        Task<Document> LoadAsync(
            string filePath, CancellationToken cancellationToken);
        Task<Document> ReloadAsync(
            Document document, CancellationToken cancellationToken);
        Task<Document> RenameAsync(
            Document document, string newFilePath, CancellationToken cancellationToken);
    }
}
