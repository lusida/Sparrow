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

        void Register<TContribution>() where TContribution : IContribution;
        bool Unregister<TContribution>() where TContribution : IContribution;
    }
}
