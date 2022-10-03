using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    internal class WorkspaceStorager : IWorkspaceStorager
    {
        private readonly Workspace _workspace;
        private readonly ConcurrentDictionary<string, string> _projectMaps;
        private readonly ConcurrentDictionary<string, Project> _projects;
        private readonly ConcurrentDictionary<string, string> _documentMaps;
        private readonly ConcurrentDictionary<string, Document> _documents;

        private readonly IDocumentServiceFactory _documentServiceFactory;

        public WorkspaceStorager(Workspace workspace)
        {
            _workspace = workspace;

            _projectMaps = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _projects = new ConcurrentDictionary<string, Project>(StringComparer.OrdinalIgnoreCase);

            _documentMaps = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _documents = new ConcurrentDictionary<string, Document>(StringComparer.OrdinalIgnoreCase);

            _documentServiceFactory = workspace.GetRequiredService<IDocumentServiceFactory>();
        }

        public Document? GetDocument(string documentId)
        {
            if (_documents.TryGetValue(documentId, out var document))
            {
                return document;
            }

            return null;
        }

        public Document? GetDocumentFromFilePath(string filePath)
        {
            if (_documentMaps.TryGetValue(filePath, out var documentId))
            {
                if (_documents.TryGetValue(documentId, out var document))
                {
                    return document;
                }
            }

            return null;
        }

        public ImmutableArray<Document> GetDocuments(string[] documentIds)
        {
            var builder = ImmutableArray.CreateBuilder<Document>();

            foreach (var documentId in documentIds)
            {
                if (_documents.TryGetValue(documentId, out var document))
                {
                    builder.Add(document);
                }
            }

            return builder.ToImmutableArray();
        }

        public Project? GetProject(string projectId)
        {
            if (_projects.TryGetValue(projectId, out var project))
            {
                return project;
            }

            return null;
        }

        public Project? GetProjectFromFilePath(string filePath)
        {
            if (_projectMaps.TryGetValue(filePath, out var projectId))
            {
                if (_projects.TryGetValue(projectId, out var project))
                {
                    return project;
                }
            }

            return null;
        }

        public ImmutableArray<Project> GetProjects()
        {
            return _projects.Values.ToImmutableArray();
        }

        public async Task<Document> LoadDocumentAsync(
            Project project, string documentFile, CancellationToken cancellationToken)
        {
            var info = new FileInfo(documentFile);

            var loader = _documentServiceFactory.GetRequiredService<IDocumentLoader>(info.Extension);

            var document = await loader.LoadAsync(info.FullName, cancellationToken);

            if (_documents.TryAdd(document.Id, document))
            {
                _documentMaps.AddOrUpdate(
                    documentFile, document.Id, (k, v) => document.Id);

                var analyzers = _documentServiceFactory.GetServices<IDocumentAnalyzer>(info.Extension);

                foreach (var analyzer in analyzers)
                {
                    await analyzer.AnalyzeAsync(document, cancellationToken);
                }
            }

            return document;
        }

        public async Task<Project> LoadProjectAsync(
            string projectFile, CancellationToken cancellationToken)
        {
            var project = new Project(
                _workspace,
                projectFile,
                Guid.NewGuid().ToString());

            if (_projects.TryAdd(project.Id, project))
            {
                if (_projectMaps.TryAdd(project.FilePath, project.Id))
                {
                    var analyzers = _workspace.GetServices<IProjectAnalyzer>();

                    foreach (var analyzer in analyzers.OrderBy(m => m.Order))
                    {
                        await analyzer.AnalyzeAsync(project, cancellationToken);
                    }
                }
            }

            return project;
        }

        public async Task<Document> ReloadDocumentAsync(
            Document document, CancellationToken cancellationToken)
        {
            var info = new FileInfo(document.FilePath);

            var loader = _documentServiceFactory.GetRequiredService<IDocumentLoader>(info.Extension);

            var newDocument = await loader.ReloadAsync(document, cancellationToken);

            if (_documents.TryUpdate(newDocument.Id, newDocument, document))
            {
                var analyzers = _documentServiceFactory.GetServices<IDocumentAnalyzer>(info.Extension);

                foreach (var analyzer in analyzers)
                {
                    await analyzer.AnalyzeAsync(newDocument, cancellationToken);
                }

                return newDocument;
            }

            return document;
        }

        public async Task<Project> ReloadProjectAsync(
            Project project, CancellationToken cancellationToken)
        {
            var analyzers = _workspace.GetServices<IProjectAnalyzer>();

            foreach (var analyzer in analyzers)
            {
                await analyzer.AnalyzeAsync(project, cancellationToken);
            }

            return project;
        }

        public async Task<Document> RenameDocumentAsync(
            Document document, string newName, CancellationToken cancellationToken)
        {
            var newFilePath = Path.Combine(document.DirectoryPath, newName);

            var info = new FileInfo(newFilePath);

            var loader = _documentServiceFactory.GetRequiredService<IDocumentLoader>(info.Extension);

            var newDocument = await loader.RenameAsync(document, newFilePath, cancellationToken);

            if (_documents.TryUpdate(newDocument.Id, newDocument, document))
            {
                var analyzers = _documentServiceFactory.GetServices<IDocumentAnalyzer>(info.Extension);

                foreach (var analyzer in analyzers)
                {
                    await analyzer.AnalyzeAsync(newDocument, cancellationToken);
                }

                var renamers = _documentServiceFactory.GetServices<IDocumentRenamer>(info.Extension);

                foreach (var renamer in renamers)
                {
                    await renamer.RenameAsync(document, newDocument, cancellationToken);
                }

                return newDocument;
            }

            return document;
        }

        public async Task<Project> RenameProjectAsync(
            Project project, string newName, CancellationToken cancellationToken)
        {
            var newFilePath = Path.Combine(project.DirectoryPath, newName);

            var newProject = new Project(_workspace, newFilePath, project.Id);

            var analyzers = _workspace.GetServices<IProjectAnalyzer>();

            foreach (var analyzer in analyzers)
            {
                await analyzer.AnalyzeAsync(project, cancellationToken);
            }

            var renamers = _workspace.GetServices<IProjectRenamer>();

            foreach (var renamer in renamers.OrderBy(m => m.Order))
            {
                await renamer.RenameAsync(project, newProject, cancellationToken);
            }

            return project;
        }

        public Task<bool> RemoveProjectAsync(
            Project project, CancellationToken cancellationToken)
        {
            if (_projectMaps.TryRemove(project.FilePath, out var projectId))
            {
                if (_projects.TryRemove(projectId, out var p))
                {

                    return true;
                }
            }

            return false;
        }

        public Task<bool> RemoveDocumentAsync(
            Document document, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _documentMaps.Clear();
            _documents.Clear();

            _projectMaps.Clear();
            _projects.Clear();
        }
    }
}
