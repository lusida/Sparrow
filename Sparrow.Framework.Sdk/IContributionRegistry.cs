using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    /// <summary>
    /// 贡献点注册器
    /// </summary>
    public interface IContributionRegistry
    {
        IContributionHost Add(IContributionHost host);
        bool Remove(IContributionHost host);
        IContributionHost Get(string rootId);

        void Register<TContribution>(
            string rootId, string? parentId = null) where TContribution : IContribution;
        bool Unregister<TContribution>(
            string rootId) where TContribution : IContribution;
    }
}
