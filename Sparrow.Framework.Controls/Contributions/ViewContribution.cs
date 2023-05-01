using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Controls
{
    public abstract class ViewContribution<TComponent>
        : AppContribution where TComponent : ComponentBase
    {
        protected ViewContribution(string id) : base(id)
        {
            this.ComponentType = typeof(TComponent);
        }

        public Type ComponentType { get; }
    }
}
