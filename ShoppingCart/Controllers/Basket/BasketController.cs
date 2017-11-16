using System.Web.Mvc;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.Order;
using ShoppingCart.Services.Basket;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Controllers.Basket
{
    public class BasketController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IBasketService _basketService;

        public BasketController() : this(UserSessionService.Instance(), new BasketService(new OrderRepository(IoC.Instance().For<IDatabase>()), UserSessionService.Instance())) { }

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
                Voucher = _userSessionService.GetVoucherForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
            };

            return View(response);
        }

        public ActionResult Summary()
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var response = new BasketControllerSummaryData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
            };

            return View(response);
        }

        public ActionResult History()
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var previousOrdersResponse = _basketService.GetPreviousOrders(_userSessionService.GetUserByUserToken(Session["UserId"].ToString()));

            if (previousOrdersResponse.HasError)
                Redirect("/Basket");

            var response = new BasketControllerHistoryData
            {
                BasketDetails = previousOrdersResponse.BasketDetails,
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
        };

            return View(response);
        }

        public ActionResult Saved()
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var previousOrdersResponse = _basketService.GetSavedOrders(_userSessionService.GetUserByUserToken(Session["UserId"].ToString()));

            if (previousOrdersResponse.HasError)
                Redirect("/Basket");

            var response = new BasketControllerSavedData
            {
                BasketDetails = previousOrdersResponse.BasketDetails,
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
            };

            return View(response);
        }

        [HttpPost]
        public ActionResult Save()
        {
            if (Session["UserId"] == null)
                return Redirect("/Basket/Saved");

            var basketCheckoutResponse = _basketService.Save(Session["UserId"]?.ToString(), OrderStatus.Partial);

            if (!basketCheckoutResponse.HasError)
                return Redirect("/Basket/Saved");

            if (basketCheckoutResponse.Error.Code == ErrorCodes.UserNotLoggedIn)
                return Redirect("/Login");

            return Redirect("/Basket/Saved");
        }

        [HttpPost]
        public ActionResult Checkout(DeliveryType delivery, string voucher)
        {
            if (Session["UserId"] == null)
                return Redirect("/Basket");

            var basketCheckoutResponse = _basketService.Checkout(delivery, voucher, Session["UserId"]?.ToString(), OrderStatus.Complete);

            if (!basketCheckoutResponse.HasError)
                return Redirect("/Basket/Summary");

            if (basketCheckoutResponse.Error.Code == ErrorCodes.UserNotLoggedIn)
                return Redirect("/Login");

            return Redirect("/Basket");
        }

        [HttpPost]
        public ActionResult Apply(int basketId)
        {
            var selectedBasket = _basketService.GetBasketById(basketId);

            if (selectedBasket.HasError)
                return Redirect("/Basket");


            var mappedBasket = new Services.UserSession.Basket
            {
                Total = selectedBasket.Basket.Total,
                Items = selectedBasket.Basket.Orders.ConvertAll(orderDetails => new BasketItem
                {
                    Pizza = orderDetails.Order.Pizza,
                    Size = orderDetails.Order.Size,
                    ExtraToppings = orderDetails.Toppings.ConvertAll(x => x.Topping),
                    Total = orderDetails.Total
                })
            };

            _userSessionService.ClearBasket(Session["UserId"].ToString());
            _userSessionService.SetBasket(Session["UserId"].ToString(), mappedBasket);

            return Redirect("/Basket");
        }
    }
}