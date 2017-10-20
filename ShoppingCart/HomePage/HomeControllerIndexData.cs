using System.Collections.Generic;
using ShoppingCart.PizzaPrice;
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
    }
}