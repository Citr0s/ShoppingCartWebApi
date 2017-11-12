using System.Collections.Generic;

namespace ShoppingCart.Data.Order
{
    public class SaveOrderRequest
    {
        public SaveOrderRequest()
        {
            Orders = new List<Order>();
        }

        public int UserId { get; set; }
        public string DeliveryType { get; set; }
        public string Voucher { get; set; }
        public List<Order> Orders { get; set; }
        public int GrandTotal { get; set; }
    }
}