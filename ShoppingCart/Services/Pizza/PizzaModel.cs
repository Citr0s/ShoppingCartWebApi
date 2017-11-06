using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.Services.Size;

namespace ShoppingCart.Services.Pizza
{
    public class PizzaModel
    {
        public string Name { get; set; }
        public Dictionary<SizeModel, Money> Sizes { get; set; }
    }
}