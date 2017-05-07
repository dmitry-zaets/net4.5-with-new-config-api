using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Extensions.Configuration;
using Net45WithNetCoreConfigApi.Extensions;
using Net45WithNetCoreConfigApi.Configurations.Mailing;
using Net45WithNetCoreConfigApi.Configurations.Feed;

namespace Net45WithNetCoreConfigApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(HttpRuntime.AppDomainAppPath, "configs"))
                .AddJsonFile("settings.json", optional: false, reloadOnChange: true);

            var configuration = configurationBuilder.Build();

            var builder = new ContainerBuilder();
            builder.RegisterOptions();

            builder.RegisterConfigurationOptions<MailingOptions>(configuration.GetSection("mailing"));
            builder.RegisterConfigurationOptions<FeedOptions>(configuration.GetSection("feed"));

            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
