using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;

namespace ShoppingCart.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            IoC.Instance().Register<IDatabase>(new NhibernateDatabase());
        }
    }
}
