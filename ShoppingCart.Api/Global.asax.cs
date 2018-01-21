using System.Web;
using System.Web.Http;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;

namespace ShoppingCart.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IoC.Instance().Register<IDatabase>(new NhibernateDatabase());
        }
    }
}
