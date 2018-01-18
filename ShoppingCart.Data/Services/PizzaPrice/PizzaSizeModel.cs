using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Services.Size;
using ShoppingCart.Data.Services.Topping;

namespace ShoppingCart.Data.Services.PizzaPrice
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