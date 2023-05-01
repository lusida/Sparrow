using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public interface IContributionHost
    {
        string RootId { get; }

        void Register<TContribution>(
            TContribution contribution, string? parentId = null) where TContribution : IContribution;
        bool Unregister<TContribution>(
            TContribution contribution) where TContribution : IContribution;
    }
}
