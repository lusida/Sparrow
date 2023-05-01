using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Common
{
    /// <summary>
    /// 自动注册标记特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InjectionAttribute : DetectionAttribute
    {
        public InjectionAttribute(
            ServiceLifetime lifetime = ServiceLifetime.Singleton) : this(null, lifetime) { }

        public InjectionAttribute(
            Type? serviceType, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            this.ServiceType = serviceType;

            this.Lifetime = lifetime;
        }

        public Type? ServiceType { get; }
        public ServiceLifetime Lifetime { get; }
    }

    /// <summary>
    /// 自动注册标记特性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InjectionAttribute<T> : InjectionAttribute where T : class
    {
        public InjectionAttribute(
            ServiceLifetime lifetime = ServiceLifetime.Singleton) : base(typeof(T), lifetime) { }
    }
}
