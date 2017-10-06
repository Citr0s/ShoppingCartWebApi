using System.Web.Mvc;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Pizza;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaService _pizzaService;

        public HomeController() : this(new PizzaService(new GetPizzaRepository(new JsonDatabase()))) { }

        public HomeController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        public ActionResult Index()
        {
            var pizzas = _pizzaService.GetAll();
            return View(pizzas);
        }
    }
}