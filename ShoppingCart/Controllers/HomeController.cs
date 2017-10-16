using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Pizza;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaService _pizzaService;

        public HomeController() : this(new PizzaService(new PizzaRepository(new NhibernateDatabase()))) { }

        public HomeController(IPizzaService pizzaService)
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
        public List<PizzaModel> Pizzas { get; set; }
    }
}