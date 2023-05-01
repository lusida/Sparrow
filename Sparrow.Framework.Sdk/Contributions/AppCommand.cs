using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public abstract class AppCommand : AppContribution
    {
        protected AppCommand(string id) : base(id)
        {

        }

        /// <summary>
        /// 判断是否可执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> CanExecuteAsync(
            object parameter, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task ExecuteAsync(
            object parameter, CancellationToken cancellationToken = default);
    }
}
