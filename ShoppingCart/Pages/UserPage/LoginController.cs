using System.Web.Mvc;

namespace ShoppingCart.Pages.UserPage
{
    public class LoginController : Controller
    {
        public LoginController()
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return Json("");
        }
    }
}