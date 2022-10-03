using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace Sparrow.LowCodeAnalysis
{
    public abstract class Document : IServiceProvider
    {
        protected Document(
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

        [YamlIgnore]
        public string Id { get; }
        [YamlIgnore]
        public string FilePath { get; }
        [YamlIgnore]
        public string Extension { get; }
        [YamlIgnore]
        public string DirectoryPath { get; }
        public string Name { get; }
        public int Version { get; }
        [YamlIgnore]
        public DocumentType Type { get; }
        [YamlIgnore]
        public Project Project { get; }

        public object? GetService(Type serviceType)
        {
            return null;
        }
    }
}
