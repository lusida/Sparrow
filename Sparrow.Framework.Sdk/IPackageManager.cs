using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public interface IPackageManager
    {
        /// <summary>
        /// 初始化包管理器
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InitializeAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 安装包
        /// </summary>
        /// <param name="packageFile"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> InstallAsync(
            string packageFile, CancellationToken cancellationToken);

        /// <summary>
        /// 卸载包
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UninstallAsync(
            string packageId,CancellationToken cancellationToken);
    }
}
