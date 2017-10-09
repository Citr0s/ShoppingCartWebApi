using ShoppingCart.Pizza;
using ShoppingCart.PizzaPrice;
using ShoppingCart.Size;
using ShoppingCart.Topping;

namespace ShoppingCart.Factories
{
    public interface IHomeControllerFactory
    {
        IPizzaService PizzaService { get; set; }
        ISizeService SizeService { get; set; }
        IToppingService ToppingService { get; set; }
        IPizzaPriceService PizzaPriceService { get; set; }
    }
}