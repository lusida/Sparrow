using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparrow.Modules.Extensions
{
    [AppModule]
    internal class ExtensionModule : IAppModule
    {
        private readonly IPackageManager _manager;

        public ExtensionModule(IPackageManager manager)
        {
            _manager = manager;
        }

        public int Order { get; } = Int32.MaxValue;

        public async void Initialize()
        {
            //TODO:加载插件
            await _manager.InitializeAsync(default);
        }
    }
}
