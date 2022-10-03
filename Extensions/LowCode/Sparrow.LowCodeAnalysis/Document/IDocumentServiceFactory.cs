using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.LowCodeAnalysis
{
    public interface IDocumentServiceFactory
    {
        TService GetRequiredService<TService>(string extension) where TService : IDocumentService;
        TService? GetService<TService>(string extension) where TService : IDocumentService;
        IEnumerable<TService> GetServices<TService>(string extension) where TService : IDocumentService;
    }
}
