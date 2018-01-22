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
            ApiSizes = new List<ApiSizeModel>();
            Toppings = new List<ToppingModel>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<SizeModel, Money> Sizes { get; set; }
        public List<ApiSizeModel> ApiSizes { get; set; }
        public List<ToppingModel> Toppings { get; set; }
    }

    public class ApiSizeModel
    {
        public SizeModel Size { get; set; }
        public Money Price { get; set; }
    }
}