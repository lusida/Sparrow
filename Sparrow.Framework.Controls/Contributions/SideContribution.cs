using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Controls
{
    public abstract class SideContribution<TComponent>
        : ViewContribution<TComponent> where TComponent : ComponentBase
    {
        protected SideContribution(string id) : base(id)
        {

        }
    }
}
