using System.Web.Mvc;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Controllers.Basket
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
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
            };

            return View(response);
        }

        [HttpPost]
        public ActionResult Checkout(DeliveryType delivery, string voucher)
        {
            if (Session["UserId"] == null)
                Redirect("Basket");

            throw new System.NotImplementedException();
        }
    }
}