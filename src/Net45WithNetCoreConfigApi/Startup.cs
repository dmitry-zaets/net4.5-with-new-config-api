using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Net45WithNetCoreConfigApi.Startup))]

namespace Net45WithNetCoreConfigApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
