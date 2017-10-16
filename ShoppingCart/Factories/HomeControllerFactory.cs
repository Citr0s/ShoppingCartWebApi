using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
using ShoppingCart.Pizza;
using ShoppingCart.PizzaPrice;
using ShoppingCart.Size;
using ShoppingCart.Topping;

namespace ShoppingCart.Factories
{
    public class HomeControllerFactory : IHomeControllerFactory
    {
        public IPizzaService PizzaService { get; set; }
        public ISizeService SizeService { get; set; }
        public IToppingService ToppingService { get; set; }
        public IPizzaPriceService PizzaPriceService { get; set; }

        public HomeControllerFactory()
        {
            PizzaService = new PizzaService(new PizzaRepository(new NhibernateDatabase()));
            SizeService = new SizeService(new SizeRepository(new NhibernateDatabase()));
            ToppingService = new ToppingService(new ToppingRepository(new NhibernateDatabase()));
            PizzaPriceService = new PizzaPriceService(new PizzaSizeRepository(new NhibernateDatabase()));
        }
    }
}