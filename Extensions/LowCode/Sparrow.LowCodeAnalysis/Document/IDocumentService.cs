using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public interface IDocumentService
    {
        int Order { get; }
        string Extension { get; }
    }
}
