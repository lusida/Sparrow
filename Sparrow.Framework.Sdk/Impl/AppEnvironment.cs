using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    internal class AppEnvironment : IAppEnvironment
    {
        public AppEnvironment(IConfiguration configuration)
        {
            this.Title = "Tiny Studio";

            this.BaseDirectory = AppContext.BaseDirectory;

            this.BuiltDirectory = Path.Combine(this.BaseDirectory, "extensions");

            this.AppDirectoryName = ".sp";

            this.UserProfileDirectory =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    this.AppDirectoryName);

            this.AppDataDirectory =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    this.AppDirectoryName);

            this.ExtensionDirectory = Path.Combine(this.AppDataDirectory, "extensions");

            if (!Directory.Exists(this.BuiltDirectory))
            {
                Directory.CreateDirectory(this.BuiltDirectory);
            }

            if (!Directory.Exists(this.UserProfileDirectory))
            {
                Directory.CreateDirectory(this.UserProfileDirectory);
            }

            if (!Directory.Exists(this.AppDataDirectory))
            {
                Directory.CreateDirectory(this.AppDataDirectory);
            }

            if (!Directory.Exists(this.ExtensionDirectory))
            {
                Directory.CreateDirectory(this.ExtensionDirectory);
            }
        }

        public string Title { get; }

        public string BaseDirectory { get; }

        public string BuiltDirectory { get; }

        public string ExtensionDirectory { get; }

        public string AppDirectoryName { get; }

        public string UserProfileDirectory { get; }

        public string AppDataDirectory { get; }
    }
}
