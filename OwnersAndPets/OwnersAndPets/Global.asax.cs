using OwnersAndPets.App_Start;
using OwnersAndPets.Implementation;
using OwnersAndPets.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OwnersAndPets
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ObjectLocator.Configure(x =>
            {
                x.For<IOwnerAccessor>().Singleton().Use<OwnerAccessor>();
                x.Policies.FillAllPropertiesOfType<IOwnerAccessor>();
                
                //x.For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
            });
        }
    }
}
