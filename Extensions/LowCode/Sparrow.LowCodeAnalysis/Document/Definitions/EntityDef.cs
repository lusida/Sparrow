using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public class EntityDef : DocumentDef
    {
        public EntityDef()
        {
            this.Fields = new List<FieldDef>();
        }

        public List<FieldDef> Fields { get; set; }
    }
}
