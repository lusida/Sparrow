using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Net.Remoting
{
    public class RemoteResult
    {
        public static RemoteResult Void =
            new RemoteResult(typeof(void).AssemblyQualifiedName ?? "", "", true, "");

        public static RemoteResult Error(string message)
        {
            return new RemoteResult(String.Empty, String.Empty, false, message);
        }

        public RemoteResult(
            string typeName, string value, bool isSuccess, string message)
        {
            this.TypeName = typeName;
            this.Value = value;
            this.IsSuccess = isSuccess;
            this.Message = message;
        }

        public bool IsSuccess { get; set; }
        public string TypeName { get; set; }
        public string Value { get; set; }
        public string Message { get; set; }
    }
}
