using System.Collections.Generic;
using ShoppingCart.Core.Money;

namespace ShoppingCart.Data.Order
{
    public class BasketDetails
    {
        public BasketDetails()
        {
            Orders = new List<OrderDetails>();
            Total = Money.From(0);
        }

        public Money Total { get; set; }
        public BasketRecord Basket { get; set; }
        public List<OrderDetails> Orders { get; set; }
    }
}