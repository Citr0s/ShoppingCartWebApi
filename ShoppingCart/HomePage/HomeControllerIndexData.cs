using System.Collections.Generic;
using ShoppingCart.PizzaPrice;
using ShoppingCart.Size;
using ShoppingCart.Topping;
using ShoppingCart.UserSession;

namespace ShoppingCart.HomePage
{
    public class HomeControllerIndexData 
    {
        public HomeControllerIndexData()
        {
            Pizzas = new List<PizzaSizeModel>();
            BasketItems = new List<BasketItem>();
        }

        public List<PizzaSizeModel> Pizzas { get; set; }
        public List<BasketItem> BasketItems { get; set; }
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