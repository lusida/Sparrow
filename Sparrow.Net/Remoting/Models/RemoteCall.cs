using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    public class RemoteCall
    {
        internal RemoteCall(
            bool isCallback,
            string typeName,
            string methodName,
            Dictionary<string, RemoteResult> parameters)
        {
            this.IsCallback = isCallback;
            this.TypeName = typeName;
            this.MethodName = methodName;
            this.Parameters = parameters;
        }

        public bool IsCallback { get; set; }
        public string TypeName { get; set; }
        public string MethodName { get; set; }
        public Dictionary<string, RemoteResult> Parameters { get; set; }
    }
}
