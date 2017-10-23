using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
using ShoppingCart.PizzaPrice;
using ShoppingCart.Size;
using ShoppingCart.Topping;
using ShoppingCart.UserSession;

namespace ShoppingCart.HomePage
{
    public class HomeController : Controller
    {
        private readonly IPizzaSizeService _pizzaSizeService;
        private readonly IToppingService _toppingService;
        private readonly ISizeService _sizeService;
        private readonly IUserSessionService _userSessionService;

        public HomeController() : this(new PizzaSizeService(new PizzaSizeRepository(), new PizzaToppingRepository()), new ToppingService(new ToppingRepository()), new SizeService(new SizeRepository()), UserSessionService.Instance()) { }

        public HomeController(IPizzaSizeService pizzaSizeService, IToppingService toppingService, ISizeService sizeService, IUserSessionService userSessionService)
        {
            _pizzaSizeService = pizzaSizeService;
            _toppingService = toppingService;
            _sizeService = sizeService;
            _userSessionService = userSessionService;
        }

        public ActionResult Index()
        {
            var response = new HomeControllerIndexData
            {
                Pizzas = _pizzaSizeService.GetAll().Pizzas,
                Form = new FormModel
                {
                    ExtraToppings = _toppingService.GetAll().Toppings.ToDictionary(x => x, y => false),
                    Sizes = _sizeService.GetAll().Sizes.ToDictionary(x => x, y => false)
                }
            };

            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();
            else
                response.BasketItems = _userSessionService.GetBasketForUser(Session["UserId"].ToString());

            return View(response);
        }

        [HttpPost]
        public ActionResult AddPizzaToBasket(int pizzaId, int sizeId, List<string> extraToppings)
        {
            var parsedExtraToppings = extraToppings.Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();

            var basketItem = new BasketItem
            {
                PizzaId = pizzaId,
                Size = sizeId,
                ExtraToppings = parsedExtraToppings
            };

            _userSessionService.AddItemToBasket(Session["UserId"].ToString(), basketItem);

            return new RedirectResult("/");
        }
    }
}