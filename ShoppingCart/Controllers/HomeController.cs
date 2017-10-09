using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCart.Pizza;
using ShoppingCart.Factories;
using ShoppingCart.PizzaPrice;
using ShoppingCart.Size;
using ShoppingCart.Topping;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaService _pizzaService;
        private readonly ISizeService _sizeService;
        private readonly IToppingService _toppingService;
        private readonly IPizzaPriceService _pizzaPriceService;

        public HomeController() : this(new HomeControllerFactory()) { }

        public HomeController(IHomeControllerFactory homeControllerFactory)
        {
            _pizzaService = homeControllerFactory.PizzaService;
            _sizeService = homeControllerFactory.SizeService;
            _toppingService = homeControllerFactory.ToppingService;
            _pizzaPriceService = homeControllerFactory.PizzaPriceService;
        }

        public ActionResult Index()
        {
            var data = new HomeControllerData
            {
                Pizzas = _pizzaService.GetAll().Pizzas,
                Sizes = _sizeService.GetAll().Sizes,
                Toppings = _toppingService.GetAll().Toppings,
                PizzaPrices = _pizzaPriceService.GetAll().PizzaPrices,
            };
            return View(data);
        }
    }

    public class HomeControllerData
    {
        public List<PizzaModel> Pizzas { get; set; }
        public List<PizzaPriceModel> PizzaPrices { get; set; }
        public List<SizeModel> Sizes { get; set; }
        public List<ToppingModel> Toppings { get; set; }
    }
}