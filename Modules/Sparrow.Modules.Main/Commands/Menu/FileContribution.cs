using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Modules.Main
{
    [Injection]
    internal class FileContribution : AppContribution
    {
        public FileContribution() : base(ContributionIds.APP_MENU_FILE_ID)
        {
            this.Title = "File";
        }
    }
}
