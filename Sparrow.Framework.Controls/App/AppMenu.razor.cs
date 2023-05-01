using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace Sparrow.Framework.Controls
{
    public partial class AppMenu
    {
        private Menu _menu = null!;
        [Parameter]
        public ObservableCollection<AppMenuItem> Roots { get; set; } = null!;
        [Parameter]
        public EventCallback<string> Click { get; set; }

        public Task RefreshAsync()
        {
            return this.InvokeAsync(() =>
            {
                this.StateHasChanged();
            });
        }

        private void OnMenuItemClick(AntDesign.MenuItem item)
        {
            this.Click.InvokeAsync(item.Key);
        }

        private void OnSubMenuClick(SubMenu item)
        {

        }
    }
}
