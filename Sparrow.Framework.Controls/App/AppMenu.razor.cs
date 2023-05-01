using System.Collections.Concurrent;

namespace Sparrow.Framework.Controls
{
    public partial class AppMenu
    {
        private Menu _menu = null!;

        private readonly List<AppMenuItem> _roots;
        private readonly ConcurrentDictionary<string, AppMenuItem> _items;

        public AppMenu()
        {
            _roots = new List<AppMenuItem>();

            _items = new ConcurrentDictionary<string, AppMenuItem>();
        }

        public void Add(
            AppContribution contribution, string? parentId)
        {
            var item = new AppMenuItem(contribution);

            if (parentId != null &&
                _items.TryGetValue(parentId, out var parent))
            {

                if (_items.TryAdd(contribution.Id, item))
                {
                    parent.Children.Add(item);

                    item.Parent = parent;
                }
            }
            else
            {
                if (_items.TryAdd(contribution.Id, item))
                {
                    _roots.Add(item);
                }
            }
        }

        public bool Remove(AppContribution contribution)
        {
            if (_items.TryRemove(contribution.Id, out var item))
            {
                if (item.Parent == null)
                {
                    return _roots.Remove(item);
                }
                else
                {
                    return item.Parent.Children.Remove(item);
                }
            }

            return false;
        }

        public Task RefreshAsync()
        {
            return this.InvokeAsync(() =>
            {
                this.StateHasChanged();
            });
        }

        private async void OnMenuItemClick(AntDesign.MenuItem item)
        {
            if (_items.TryGetValue(item.Key, out var appItem))
            {
                await appItem.ExecuteAsync(item);
            }
        }

        private void OnSubMenuClick(SubMenu item)
        {

        }
    }
}
