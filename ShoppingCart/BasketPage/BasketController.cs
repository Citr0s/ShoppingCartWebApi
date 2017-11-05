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
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var response = new BasketControllerIndexData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString())
            };

            return View(response);
        }

        public ActionResult Checkout()
        {
            throw new System.NotImplementedException();
        }
    }
}