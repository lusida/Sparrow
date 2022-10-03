using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WorkspaceServiceAttribute : Attribute
    {
        public WorkspaceServiceAttribute(
            ServiceLifetime lifetime = ServiceLifetime.Singleton) : this(null, lifetime)
        {

        }

        public WorkspaceServiceAttribute(
            Type? serviceType, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public Type? ServiceType { get; }
        public ServiceLifetime Lifetime { get; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WorkspaceServiceAttribute<TServicce> : WorkspaceServiceAttribute where TServicce : class
    {
        public WorkspaceServiceAttribute(
            ServiceLifetime lifetime = ServiceLifetime.Singleton) : base(typeof(TServicce), lifetime)
        {
        }
    }
}
