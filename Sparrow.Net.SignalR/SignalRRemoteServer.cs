using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Sparrow.Net.Remoting;

namespace Sparrow.Net.SignalR
{
    public class SignalRRemoteServer : RemoteServer
    {
        private WebApplicationBuilder? _builder;
        public SignalRRemoteServer(string hostUrl, string[] args) : base(hostUrl, args)
        {

        }

        protected override IServiceCollection Initialize(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);

            _builder.Services.AddSignalR();

            return _builder.Services;
        }

        public override void Run()
        {
            if (_builder != null)
            {
                var app = _builder.Build();

                app.MapHub<RemoteHub>("/remote");

                app.Run();
            }
        }
    }
}
