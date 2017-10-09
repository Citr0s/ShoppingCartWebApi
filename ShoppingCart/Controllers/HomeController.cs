using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCart.Pizza;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaPrice;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
using ShoppingCart.Size;
using ShoppingCart.Topping;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaService _pizzaService;
        private readonly ISizeService _sizeService;
        private readonly IToppingService _toppingService;

        public HomeController() 
            : this(new PizzaService(new GetPizzaPriceRepository(new NhibernateDatabase())),
                  new SizeService(new GetSizeRepository(new NhibernateDatabase())),
                new ToppingService(new GetToppingRepository(new NhibernateDatabase()))) { }

        public HomeController(IPizzaService pizzaService, ISizeService sizeService, IToppingService toppingService)
        {
            _sizeService = sizeService;
            _toppingService = toppingService;
            _pizzaService = pizzaService;
        }

        public ActionResult Index()
        {
            var data = new HomeControllerData
            {
                Pizzas = _pizzaService.GetAll().Pizzas,
                Sizes = _sizeService.GetAll().Sizes,
                Toppings = _toppingService.GetAll().Toppings
            };
            return View(data);
        }
    }

    public class HomeControllerData
    {
        public List<PizzaPriceModel> Pizzas { get; set; }
        public List<SizeModel> Sizes { get; set; }
        public List<ToppingModel> Toppings { get; set; }
    }
}