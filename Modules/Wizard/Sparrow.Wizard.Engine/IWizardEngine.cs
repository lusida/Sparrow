using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Wizard.Engine
{
    public interface IWizardEngine
    {
        Task<IEnumerable<WizardItem>> GetItemsAsync(CancellationToken cancellationToken);
    }
}
