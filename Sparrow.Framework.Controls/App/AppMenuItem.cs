using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuItem = AntDesign.MenuItem;

namespace Sparrow.Framework.Controls
{
    public class AppMenuItem : MenuItem
    {
        [Parameter]
        public AppCommand? Command { get; set; }
    }
}
