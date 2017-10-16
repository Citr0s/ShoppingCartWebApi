using System.Collections.Generic;

namespace ShoppingCart.UserSession
{
    public class Basket
    {
        public Basket()
        {
            Items = new List<BasketItem>();
        }

        public List<BasketItem> Items { get; set; }
    }
}