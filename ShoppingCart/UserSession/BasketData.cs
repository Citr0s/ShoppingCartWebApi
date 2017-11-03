using System.Collections.Generic;

namespace ShoppingCart.UserSession
{
    public class BasketData
    {
        public BasketData()
        {
            ExtraToppings = new List<int>();
        }

        public int PizzaId { get; set; }
        public int Size { get; set; }
        public List<int> ExtraToppings { get; set; }
    }
}