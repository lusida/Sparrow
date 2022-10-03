using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public class FieldDef
    {
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public string TypeId { get; set; } = "String";
        public bool IsNullable { get; set; }
        public bool IsCollection { get; set; }
        public object? DefaultValue { get; set; }
        public bool IsKey { get; set; }
        public bool IsRequired { get; set; }
        public string? RequiredError { get; set; }
    }
}
