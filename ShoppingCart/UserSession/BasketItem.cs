using System.Collections.Generic;
using ShoppingCart.Core.Money;

namespace ShoppingCart.UserSession
{
    public class BasketItem
    {
        public BasketItem()
        {
            ExtraToppings = new List<int>();
            Total = Money.From(0);
        }

        public int PizzaId { get; set; }
        public int Size { get; set; }
        public List<int> ExtraToppings { get; set; }
        public Money Total { get; set; }
    }
}