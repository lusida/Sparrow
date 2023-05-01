using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Controls
{
    internal class AppMenuItem
    {
        public AppMenuItem(AppContribution contribution)
        {
            this.Children = new List<AppMenuItem>();

            this.Contribution = contribution;
        }

        public AppMenuItem? Parent { get; set; }
        public AppContribution Contribution { get; }
        public List<AppMenuItem> Children { get; }

        public async Task ExecuteAsync(object parameter)
        {
            if (this.Contribution is AppCommand command)
            {
                if (await command.CanExecuteAsync(parameter))
                {
                    await command.ExecuteAsync(parameter);
                }
            }
        }
    }
}
