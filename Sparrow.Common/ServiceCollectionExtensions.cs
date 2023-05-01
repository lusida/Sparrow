using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Common
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加自动注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddInjections(
            this IServiceCollection services, Action<IDetectionOptions>? optionsAction = null)
        {
            var options = new DetectionOptions(services);

            optionsAction?.Invoke(options);

            var assemblyFiles = Directory.GetFiles(
                options.Directory, "Sparrow.*.dll", SearchOption.TopDirectoryOnly);

            foreach (var assemblyFile in assemblyFiles)
            {
                var assembly =
                    options.AssemblyLoadContext.LoadFromAssemblyPath(assemblyFile);

                services.AddAssembly(assembly, options);
            }

            return services;
        }

        private static IServiceCollection AddAssembly(
            this IServiceCollection services, Assembly assembly, DetectionOptions options)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                if (type.IsClass && !type.IsAbstract)
                {
                    var attribute = type.GetCustomAttribute<DetectionAttribute>();

                    if (attribute != null)
                    {
                        switch (attribute)
                        {
                            case InjectionAttribute inject:
                                {
                                    var serviceType = inject.ServiceType ?? type;

                                    switch (inject.Lifetime)
                                    {
                                        case ServiceLifetime.Singleton:
                                            {
                                                services.AddSingleton(serviceType, type);
                                            }
                                            break;
                                        case ServiceLifetime.Scoped:
                                            {
                                                services.AddScoped(serviceType, type);
                                            }
                                            break;
                                        case ServiceLifetime.Transient:
                                            {
                                                services.AddTransient(serviceType, type);
                                            }
                                            break;
                                    }
                                }
                                break;
                            default:
                                {
                                    if (options.Providers.TryGetValue(
                                        attribute.GetType(), out var provider))
                                    {
                                        provider.Configure(services, attribute, type);
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            return services;
        }
    }
}
