using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    public class RemoteResult
    {
        public RemoteResult(
            string typeName, string value)
        {
            TypeName = typeName;
            Value = value;
        }

        public string TypeName { get; set; }
        public string Value { get; set; }
    }
}
