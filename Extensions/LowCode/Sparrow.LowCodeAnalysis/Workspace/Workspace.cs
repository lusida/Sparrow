using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public class Workspace : IServiceProvider, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        public Workspace(
            IServiceProvider serviceProvider, string rootDirectory)
        {
            _serviceProvider = serviceProvider;

            this.RootDirectory = rootDirectory;

            this.Storager = new WorkspaceStorager(this);
        }

        public string RootDirectory { get; }
        internal IWorkspaceStorager Storager { get; }
        public ImmutableArray<Project> Projects => this.Storager.GetProjects();

        public Project? GetProject(string projectId)
        {
            return this.Storager.GetProject(projectId);
        }

        public Project? GetProjectFromFilePath(string projectFile)
        {
            return this.Storager.GetProjectFromFilePath(projectFile);
        }

        public Document? GetDocument(string documentId)
        {
            return this.Storager.GetDocument(documentId);
        }

        public Document? GetDocumentFromFilePath(string documentId)
        {
            return this.Storager.GetDocumentFromFilePath(documentId);
        }

        public Task<Project> AddProjectAsync(
            string projectFile, CancellationToken cancellationToken)
        {
            return this.Storager.LoadProjectAsync(projectFile, cancellationToken);
        }

        public async Task<bool> RemoveProjectAsync(
            string projectFile, CancellationToken cancellationToken)
        {
            var project = this.Storager.GetProjectFromFilePath(projectFile);

            if (project != null)
            {
                return await this.Storager.RemoveProjectAsync(project, cancellationToken);
            }

            return false;
        }

        public async Task<Project> RenameAsync(
            string projectFile, string newName, CancellationToken cancellationToken)
        {
            var project = this.Storager.GetProjectFromFilePath(projectFile);

            if (project != null)
            {
                return await this.Storager.RenameProjectAsync(project, newName, cancellationToken);
            }
            else
            {
                throw new FileNotFoundException(projectFile);
            }
        }

        public object? GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public void Dispose()
        {
            this.Storager.Dispose();
        }
    }
}
