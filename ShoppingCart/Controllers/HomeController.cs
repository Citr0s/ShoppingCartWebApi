using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Pizza;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Size;
using ShoppingCart.Size;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaService _pizzaService;
        private readonly ISizeService _sizeService;

        public HomeController() : this(new PizzaService(new GetPizzaRepository(new NhibernateDatabase())), new SizeService(new GetSizeRepository(new NhibernateDatabase()))) { }

        public HomeController(IPizzaService pizzaService, ISizeService sizeService)
        {
            _sizeService = sizeService;
            _pizzaService = pizzaService;
        }

        public ActionResult Index()
        {
            var data = new HomeControllerData
            {
                Pizzas = _pizzaService.GetAll().Pizzas,
                Sizes = _sizeService.GetAll().Sizes
            };
            return View(data);
        }
    }

    public class HomeControllerData
    {
        public List<PizzaModel> Pizzas { get; set; }
        public List<SizeModel> Sizes { get; set; }
    }
}