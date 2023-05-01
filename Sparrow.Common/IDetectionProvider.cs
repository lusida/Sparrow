using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Common
{
    public interface IDetectionProvider
    {
        void Configure(
            IServiceCollection services,
            DetectionAttribute attribute,
            Type type);
    }
}
