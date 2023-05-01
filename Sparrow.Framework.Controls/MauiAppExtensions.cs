using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Framework.Controls
{
    public static class MauiAppExtensions
    {
        public static MauiApp UseSparrow(this MauiApp app)
        {
            var modules = app.Services.GetServices<IAppModule>();

            foreach (var module in modules.OrderBy(m => m.Order))
            {
                module.Initialize();
            }

            return app;
        }
    }
}
