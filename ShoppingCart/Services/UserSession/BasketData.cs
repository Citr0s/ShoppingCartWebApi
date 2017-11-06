using System.Collections.Generic;

namespace ShoppingCart.Services.UserSession
{
    public class BasketData
    {
        public BasketData()
        {
            ExtraToppingIds = new List<int>();
        }

        public int PizzaId { get; set; }
        public int SizeId { get; set; }
        public List<int> ExtraToppingIds { get; set; }
    }
}