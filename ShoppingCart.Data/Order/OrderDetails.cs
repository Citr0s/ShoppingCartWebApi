using System.Collections.Generic;
using ShoppingCart.Core.Money;

namespace ShoppingCart.Data.Order
{
    public class OrderDetails
    {
        public OrderDetails()
        {
            Toppings = new List<OrderToppingRecord>();
            Total = Money.From(0);
        }

        public Money Total { get; set; }
        public OrderRecord Order { get; set; }
        public List<OrderToppingRecord> Toppings { get; set; }
    }
}