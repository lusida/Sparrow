using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class RemoteCallbackAttribute : Attribute
    {

    }
}
