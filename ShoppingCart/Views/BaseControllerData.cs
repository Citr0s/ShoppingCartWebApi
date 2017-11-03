using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.UserSession;

namespace ShoppingCart.Views
{
    public class BaseControllerData
    {
        public BaseControllerData()
        {
            BasketItems = new List<BasketItem>();
            Total = Money.From(0);
        }

        public List<BasketItem> BasketItems { get; set; }
        public Money Total { get; set; }
    }
}