using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.Size;

namespace ShoppingCart.Pizza
{
    public class PizzaModel
    {
        public string Name { get; set; }
        public Dictionary<SizeModel, Money> Sizes { get; set; }
    }
}