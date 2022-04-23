using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Sparrow.Net.Remoting
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RemoteServiceAttribute : Attribute
    {
        public RemoteServiceAttribute(
            ServiceLifetime lifetime = ServiceLifetime.Singleton) : this(null, lifetime)
        {

        }

        public RemoteServiceAttribute(
            Type? serviceType, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            this.ServiceType = serviceType;

            this.Lifetime = lifetime;
        }

        public Type? ServiceType { get; set; }
        public ServiceLifetime Lifetime { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RemoteServiceAttribute<T> : RemoteServiceAttribute
    {
        public RemoteServiceAttribute(
            ServiceLifetime lifetime = ServiceLifetime.Singleton) : base(typeof(T), lifetime)
        {

        }
    }
}
