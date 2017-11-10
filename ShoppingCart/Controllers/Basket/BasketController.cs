using System.Web.Mvc;
using ShoppingCart.Data.Order;
using ShoppingCart.Services.Basket;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Controllers.Basket
{
    public class BasketController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IBasketService _basketService;

        public BasketController() : this(UserSessionService.Instance(), new BasketService(new OrderRepository())) { }

        public BasketController(IUserSessionService userSessionService, IBasketService basketService)
        {
            _userSessionService = userSessionService;
            _basketService = basketService;
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

            var basketCheckoutResponse = _basketService.Checkout(delivery, voucher, Session["UserId"]?.ToString());

            if (basketCheckoutResponse.HasError)
                Redirect("Basket");

            throw new System.NotImplementedException();
        }
    }
}