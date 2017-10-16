using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.Size;
using ShoppingCart.Topping;

namespace ShoppingCart.PizzaPrice
{
    public class PizzaSizeModel
    {
        public PizzaSizeModel()
        {
            Sizes = new Dictionary<SizeModel, Money>();
        }

        public string Name { get; set; }
        public Dictionary<SizeModel, Money> Sizes { get; set; }
        public Dictionary<ToppingModel, Money> Toppings { get; set; }
    }
}