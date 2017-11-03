using System.Web.Mvc;

namespace ShoppingCart.BasketPage
{
    public class BasketController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}