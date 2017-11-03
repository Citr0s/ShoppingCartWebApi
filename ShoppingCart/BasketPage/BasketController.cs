using System.Web.Mvc;
using ShoppingCart.UserSession;

namespace ShoppingCart.BasketPage
{
    public class BasketController : Controller
    {
        private readonly IUserSessionService _userSessionService;

        public BasketController() : this(UserSessionService.Instance()) { }

        public BasketController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        public ActionResult Index()
        {
            var response = new BasketControllerIndexData
            {
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString())
            };

            return View(response);
        }
    }
}