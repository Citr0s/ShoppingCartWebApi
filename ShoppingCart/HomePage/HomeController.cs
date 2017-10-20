using System.Web.Mvc;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.PizzaPrice;
using ShoppingCart.UserSession;

namespace ShoppingCart.HomePage
{
    public class HomeController : Controller
    {
        private readonly IPizzaSizeService _pizzaSizeService;
        private readonly IUserSessionService _userSessionService;

        public HomeController() : this(new PizzaSizeService(new PizzaSizeRepository(new NhibernateDatabase()), new PizzaToppingRepository(new NhibernateDatabase())), UserSessionService.Instance()) { }

        public HomeController(IPizzaSizeService pizzaSizeService, IUserSessionService userSessionService)
        {
            _pizzaSizeService = pizzaSizeService;
            _userSessionService = userSessionService;
        }

        public ActionResult Index()
        {
            var response = new HomeControllerIndexData
            {
                Pizzas = _pizzaSizeService.GetAll().Pizzas
            };

            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();
            else
                response.BasketItems = _userSessionService.GetBasketForUser(Session["UserId"].ToString());

            return View(response);
        }

        public ActionResult AddPizzaToBasket(string pizzaName, string pizzaSize)
        {
            var basketItem = new BasketItem
            {
                Name = pizzaName,
                Size = pizzaSize
            };

            _userSessionService.AddItemToBasket(Session["UserId"].ToString(), basketItem);

            return new RedirectResult("/");
        }
    }
}