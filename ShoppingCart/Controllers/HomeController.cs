using System.Web.Mvc;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.PizzaPrice;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaSizeService _pizzaSizeService;

        public HomeController() : this(new PizzaSizeService(new PizzaSizeRepository(new NhibernateDatabase()), new PizzaToppingRepository(new NhibernateDatabase()))) { }

        public HomeController(IPizzaSizeService pizzaSizeService)
        {
            _pizzaSizeService = pizzaSizeService;
        }

        public ActionResult Index()
        {
            return View(_pizzaSizeService.GetAll().Pizzas);
        }
    }
}