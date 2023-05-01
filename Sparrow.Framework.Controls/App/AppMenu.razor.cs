using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Timers;
using MenuItem = AntDesign.MenuItem;

namespace Sparrow.Framework.Controls
{
    public partial class AppMenu
    {
        private Menu _menu = null!;

        [Parameter]
        public List<AppContribution> Items { get; set; } = new List<AppContribution>();

        private async void OnMenuItemClick(MenuItem item)
        {
            if (item is AppMenuItem menuItem &&
                menuItem.Command != null)
            {
                if (await menuItem.Command.CanExecuteAsync(menuItem))
                {
                    await menuItem.Command.ExecuteAsync(menuItem);
                }
            }
        }

        private void OnSubMenuClick(SubMenu item)
        {

        }
    }
}
