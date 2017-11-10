using System.Collections.Generic;

namespace ShoppingCart.Data.Order
{
    public class Order
    {
        public Order()
        {
            ExtraToppingIds = new List<int>();
        }

        public int PizzaId { get; set; }
        public int SizeId { get; set; }
        public List<int> ExtraToppingIds { get; set; }
        public int SubTotal { get; set; }
    }
}