using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCart.Pizza;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaPrice;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
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

        public HomeController() : this(new PizzaService(new PizzaRepository(new NhibernateDatabase())), new SizeService(new SizeRepository(new NhibernateDatabase())), new ToppingService(new ToppingRepository(new NhibernateDatabase())), new PizzaPriceService(new PizzaPriceRepository(new NhibernateDatabase()))) { }

        public HomeController(IPizzaService pizzaService, ISizeService sizeService, IToppingService toppingService, IPizzaPriceService pizzaPriceService)
        {
            _sizeService = sizeService;
            _toppingService = toppingService;
            _pizzaPriceService = pizzaPriceService;
            _pizzaService = pizzaService;
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