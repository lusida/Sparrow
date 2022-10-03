using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    internal interface IWorkspaceStorager : IDisposable
    {
        ImmutableArray<Project> GetProjects();
        ImmutableArray<Document> GetDocuments(string[] documentIds);
        Project? GetProject(string projectId);
        Project? GetProjectFromFilePath(string filePath);
        Document? GetDocument(string documentId);
        Document? GetDocumentFromFilePath(string filePath);
        Task<Project> LoadProjectAsync(
            string projectFile, CancellationToken cancellationToken);
        Task<Document> LoadDocumentAsync(
            Project project, string documentFile, CancellationToken cancellationToken);
        Task<Project> ReloadProjectAsync(
            Project project, CancellationToken cancellationToken);
        Task<Document> ReloadDocumentAsync(
            Document document, CancellationToken cancellationToken);
        Task<Project> RenameProjectAsync(
            Project project, string newName, CancellationToken cancellationToken);
        Task<Document> RenameDocumentAsync(
            Document document, string newName, CancellationToken cancellationToken);
        Task<bool> RemoveProjectAsync(Project project, CancellationToken cancellationToken);
        Task<bool> RemoveDocumentAsync(Document document, CancellationToken cancellationToken);
    }
}
