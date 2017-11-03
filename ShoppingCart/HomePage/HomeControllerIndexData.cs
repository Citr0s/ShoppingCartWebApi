using System.Collections.Generic;
using ShoppingCart.PizzaPrice;
using ShoppingCart.Size;
using ShoppingCart.Topping;
using ShoppingCart.Views;

namespace ShoppingCart.HomePage
{
    public class HomeControllerIndexData : BaseControllerData
    {
        public HomeControllerIndexData()
        {
            Pizzas = new List<PizzaSizeModel>();
        }

        public List<PizzaSizeModel> Pizzas { get; set; }
        public FormModel Form { get; set; }
    }

    public class FormModel
    {
        public FormModel()
        {
            ExtraToppings = new Dictionary<ToppingModel, bool>();
            Sizes = new Dictionary<SizeModel, bool>();
        }

        public Dictionary<ToppingModel, bool> ExtraToppings { get; set; }
        public Dictionary<SizeModel, bool> Sizes { get; set; }
    }
}