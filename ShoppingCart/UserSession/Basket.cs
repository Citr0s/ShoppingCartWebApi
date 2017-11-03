using System.Collections.Generic;
using ShoppingCart.Core.Money;

namespace ShoppingCart.UserSession
{
    public class Basket
    {
        public Basket()
        {
            Items = new List<BasketItem>();
            Total = Money.From(0);
        }

        public List<BasketItem> Items { get; set; }
        public Money Total { get; set; }
    }
}