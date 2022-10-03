using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public class Project : IDisposable
    {
        private string[] _documentIds = Array.Empty<string>();

        internal Project(
            Workspace workspace, string filePath, string id)
        {
            this.Id = id;

            this.Workspace = workspace;

            this.FilePath = filePath;

            this.Name = Path.GetFileNameWithoutExtension(filePath);

            this.DirectoryPath = Path.GetDirectoryName(filePath)!;
        }

        public string Id { get; }
        public Workspace Workspace { get; }
        public string Name { get; }
        public string FilePath { get; }
        public string DirectoryPath { get; }

        public ImmutableArray<Document> Documents =>
            this.Workspace.Storager.GetDocuments(_documentIds);

        public async Task<Document> AddDocumentAsync(
            string filePath, CancellationToken cancellationToken)
        {
            var document = await this.Workspace.Storager.LoadDocumentAsync(this, filePath, cancellationToken);

            var list = new List<string>(_documentIds);

            list.Add(document.Id);

            _documentIds = list.Distinct().ToArray();

            return document;
        }

        public Task<Document> ReloadAsync(
            string filePath, CancellationToken cancellationToken)
        {
            var document = this.Workspace.Storager.GetDocumentFromFilePath(filePath);

            if (document != null)
            {
                return this.Workspace.Storager.ReloadDocumentAsync(document, cancellationToken);
            }
            else
            {
                throw new FileNotFoundException(filePath);
            }
        }

        public Task<Document> RenameAsync(
            string filePath,string newName,CancellationToken cancellationToken)
        {
            var document = this.Workspace.Storager.GetDocumentFromFilePath(filePath);

            if (document != null)
            {
                return this.Workspace.Storager.RenameDocumentAsync(document,newName, cancellationToken);
            }
            else
            {
                throw new FileNotFoundException(filePath);
            }
        }

        public async Task<bool> RemoveDocumentAsync(
            string filePath, CancellationToken cancellationToken)
        {
            var document = this.Workspace.Storager.GetDocumentFromFilePath(filePath);

            if (document != null)
            {
                if (_documentIds.Contains(document.Id))
                {
                    if (await this.Workspace.Storager.RemoveDocumentAsync(document, cancellationToken))
                    {
                        _documentIds = _documentIds.Where(m => m != document.Id).ToArray();

                        return true;
                    }
                }
                else
                {
                    throw new KeyNotFoundException(filePath);
                }
            }

            return false;
        }

        public void Dispose()
        {

        }
    }
}
