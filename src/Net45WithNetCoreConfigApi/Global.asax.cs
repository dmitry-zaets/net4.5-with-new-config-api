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

namespace Net45WithNetCoreConfigApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(HttpRuntime.AppDomainAppPath, "configs"))
                .AddJsonFile("settings.json");

            var config = configurationBuilder.Build();

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            builder.Register(_ => new DiTestClass { Name ="My name"}).As<DiTestClass>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }

    public class DiTestClass
    {
        public string Name { get; set; }
    }
}
