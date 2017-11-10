using System.Collections.Generic;

namespace ShoppingCart.Data.Order
{
    public class SaveOrderRequest
    {
        public SaveOrderRequest()
        {
            Orders = new List<Order>();
        }

        public List<Order> Orders { get; set; }
        public int GrandTotal { get; set; }
    }
}