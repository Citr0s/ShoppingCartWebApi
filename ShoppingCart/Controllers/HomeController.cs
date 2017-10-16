using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Pizza;
using ShoppingCart.PizzaPrice;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaSizeService _pizzaService;

        public HomeController() : this(new PizzaSizeService(new PizzaSizeRepository(new NhibernateDatabase()))) { }

        public HomeController(IPizzaSizeService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        public ActionResult Index()
        {
            var data = new HomeControllerData
            {
                Pizzas = _pizzaService.GetAll().Pizzas
            };
            return View(data);
        }
    }

    public class HomeControllerData
    {
        public List<PizzaSizeModel> Pizzas { get; set; }
    }
}