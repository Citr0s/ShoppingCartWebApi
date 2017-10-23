using System.Collections.Generic;
using ShoppingCart.Core.Money;

namespace ShoppingCart.UserSession
{
    public class BasketItem
    {
        public BasketItem()
        {
            ExtraToppings = new List<int>();
        }

        public int PizzaId { get; set; }
        public int Size { get; set; }
        public Money Price { get; set; }
        public List<int> ExtraToppings { get; set; }
    }
}