using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.Services.Size;
using ShoppingCart.Services.Topping;

namespace ShoppingCart.Services.PizzaPrice
{
    public class PizzaSizeModel
    {
        public PizzaSizeModel()
        {
            Sizes = new Dictionary<SizeModel, Money>();
            Toppings = new List<ToppingModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<SizeModel, Money> Sizes { get; set; }
        public List<ToppingModel> Toppings { get; set; }
    }
}