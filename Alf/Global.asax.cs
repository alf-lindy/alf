using Alf.App_Code;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Alf
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly IUnityContainer IoC = new UnityContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ConfigureIoC();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void ConfigureIoC()
        {
            IoC.RegisterType<IParticipantRepository, ParticipantRepository>();
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Start", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}