using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public class PackageMetadata
    {
        public PackageMetadata(
            string id, 
            string name, 
            Version version, 
            string? description, 
            string author, 
            string? projectUrl, 
            string? readmeUrl)
        {
            this.Id = id;
            this.Name = name;
            this.Version = version;
            this.Description = description;
            this.Author = author;
            this.ProjectUrl = projectUrl;
            this.ReadmeUrl = readmeUrl;
        }

        public string Id { get; }
        public string Name { get; }
        public Version Version { get; }
        public string? Description { get; }
        public string Author { get; }
        public string? ProjectUrl { get; }
        public string? ReadmeUrl { get; }

        [JsonIgnore]
        public string FilePath { get; internal set; } = String.Empty;
        [JsonIgnore]
        public string DirectoryPath { get; internal set; } = String.Empty;
    }
}
