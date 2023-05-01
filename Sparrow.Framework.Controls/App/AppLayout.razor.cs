using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Controls
{
    public partial class AppLayout
    {
        [Parameter]
        public List<AppContribution> MenuItems { get; set; } = new List<AppContribution>();
    }
}
