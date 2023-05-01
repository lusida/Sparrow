using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public abstract class AppContribution
    {
        protected AppContribution(string id)
        {
            Id = id;
        }

        public string Id { get; }
        public string Title { get; init; } = string.Empty;
        public string? Icon { get; protected set; }
        public string? ShortcutKey { get; init; }
        public string? Description { get; init; }
        public object? Group { get; init; }
        public int Order { get; init; }
        public bool IsEnabled { get; protected set; }
    }
}
