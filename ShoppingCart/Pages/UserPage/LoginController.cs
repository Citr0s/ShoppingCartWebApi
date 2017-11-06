using System.Web.Mvc;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Pages.UserPage
{
    public class LoginController : Controller
    {
        private readonly IUserSessionService _userSessionService;

        public LoginController() : this(UserSessionService.Instance()) { }

        public LoginController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var response = new LoginControllerIndexData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString())
            };

            return View(response);
        }

        [HttpPost]
        public ActionResult CheckCredentials(string username, string password)
        {
            // TODO: Call UserService
            return Json("");
        }
    }
}