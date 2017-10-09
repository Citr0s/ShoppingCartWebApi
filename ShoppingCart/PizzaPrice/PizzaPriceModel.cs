using ShoppingCart.Core.Money;

namespace ShoppingCart.PizzaPrice
{
    public class PizzaPriceModel
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public Money Price { get; set; }
    }
}