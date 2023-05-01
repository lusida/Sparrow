using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Controls
{
    public abstract class ViewCommand<TComponent>
        : AppCommand where TComponent : ComponentBase
    {
        protected ViewCommand(string id) : base(id)
        {
        }
    }
}
