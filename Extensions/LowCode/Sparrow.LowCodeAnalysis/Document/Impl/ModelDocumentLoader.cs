using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    [WorkspaceService<IDocumentLoader>]
    internal class ModelDocumentLoader : IDocumentLoader
    {
        public int Order { get; }
        public string Extension { get; } = ".mdd";

        public Task<Document> LoadAsync(string filePath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Document> ReloadAsync(Document document, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Document> RenameAsync(
            Document document, string newFilePath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
