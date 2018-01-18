using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Services.Size;

namespace ShoppingCart.Data.Services.Pizza
{
    public class PizzaModel
    {
        public string Name { get; set; }
        public Dictionary<SizeModel, Money> Sizes { get; set; }
    }
}