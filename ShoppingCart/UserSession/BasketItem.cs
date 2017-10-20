using System.Collections.Generic;
using ShoppingCart.Core.Money;

namespace ShoppingCart.UserSession
{
    public class BasketItem
    {
        public BasketItem()
        {
            ExtraToppings = new List<string>();
        }

        public string Name { get; set; }
        public string Size { get; set; }
        public Money Price { get; set; }
        public List<string> ExtraToppings { get; set; }
    }
}