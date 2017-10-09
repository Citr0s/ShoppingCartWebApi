using ShoppingCart.Core.Money;

namespace ShoppingCart.Pizza
{
    public class PizzaPriceModel
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public Money Price { get; set; }
    }
}