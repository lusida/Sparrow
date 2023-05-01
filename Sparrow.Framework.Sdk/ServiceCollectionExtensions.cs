using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Sdk
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Sparrow框架支持
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddSparrow(
            this IServiceCollection services, Action<IDetectionOptions>? optionsAction = null)
        {
            services.AddSingleton<IAppEnvironment, AppEnvironment>();

            services.AddSingleton<ContributionRegistry>();

            services.AddSingleton<IContributionRegistry>(
                p => p.GetRequiredService<ContributionRegistry>());

            services.AddSingleton<IPackageManager, PackageManager>();

            services.AddInjections(optionsAction);

            return services;
        }
    }
}
