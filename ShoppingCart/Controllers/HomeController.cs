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
            var pizzas = _pizzaService.GetAll();
            var sizes = _sizeService.GetAll();
            return View(pizzas);
        }
    }
}