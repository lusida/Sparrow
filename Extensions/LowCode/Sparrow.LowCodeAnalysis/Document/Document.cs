using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Sparrow.LowCodeAnalysis
{
    public class Document : IServiceProvider
    {
        internal Document(
            Project project,
            string documentId,
            string filePath,
            DocumentType type)
        {
            this.Id = documentId;

            this.Project = project;

            this.Type = type;

            this.FilePath = filePath;

            this.Extension = Path.GetExtension(filePath);

            this.Name = Path.GetFileNameWithoutExtension(filePath);

            this.DirectoryPath = Path.GetDirectoryName(FilePath)!;
        }

        public string Id { get; }
        public string FilePath { get; }
        public string Extension { get; }
        public string DirectoryPath { get; }
        public string Name { get; }
        public int Version { get; }
        public DocumentType Type { get; }
        public Project Project { get; }

        public object? GetService(Type serviceType)
        {
            return null;
        }
    }
}
