using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Modules.Main
{
    [ContributionHost]
    internal class AppMenuContributionHost : ContributionHost
    {
        private readonly ConcurrentDictionary<string, AppMenuItem> _items;

        public AppMenuContributionHost() : base(ContributionIds.APP_MENU_ID)
        {
            Items = new ObservableCollection<AppMenuItem>();

            _items = new ConcurrentDictionary<string, AppMenuItem>();
        }

        public ObservableCollection<AppMenuItem> Items { get; private set; }

        public override void Register<TContribution>(
            TContribution contribution, string? parentId = null)
        {
            if (contribution is AppContribution contrib)
            {
                var item = new AppMenuItem(contrib);

                if (parentId != null &&
                    _items.TryGetValue(parentId, out var parent))
                {

                    if (_items.TryAdd(contrib.Id, item))
                    {
                        parent.Children.Add(item);

                        item.Parent = parent;
                    }
                }
                else
                {
                    if (_items.TryAdd(contrib.Id, item))
                    {
                        this.Items.Add(item);
                    }
                }
            }
        }

        public override bool Unregister<TContribution>(TContribution contribution)
        {
            if (contribution is AppContribution contrib &&
                _items.TryRemove(contrib.Id, out var item))
            {
                if (item.Parent == null)
                {
                    return this.Items.Remove(item);
                }
                else
                {
                    return item.Parent.Children.Remove(item);
                }
            }

            return false;
        }
    }
}
