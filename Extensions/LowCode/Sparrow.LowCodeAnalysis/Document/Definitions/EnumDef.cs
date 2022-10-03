using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public class EnumDef:DocumentDef
    {
        public EnumDef()
        {
            this.Fields = new List<FieldDef>();
        }

        public bool IsFlags { get; set; }
        public List<FieldDef> Fields { get; set; }
    }
}
