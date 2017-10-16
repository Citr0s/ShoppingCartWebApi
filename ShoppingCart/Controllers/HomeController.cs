using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaSize;
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
            return View(_pizzaService.GetAll().Pizzas);
        }
    }
}