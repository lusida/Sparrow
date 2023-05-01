using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Modules.Main
{
    public partial class AppLayout
    {
        private ObservableCollection<AppMenuItem>? _menuItems;
        [Inject]
        private IContributionRegistry _registry { get; set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var menuHost = _registry.Get<AppMenuContributionHost>(ContributionIds.APP_MENU_ID);

            _menuItems = menuHost.Items;
        }

        private void OnMenuClick(string key)
        {

        }
    }
}
