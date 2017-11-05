using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.UserSession
{
    public class BasketItem
    {
        public BasketItem()
        {
            ExtraToppings = new List<ToppingRecord>();
            Total = Money.From(0);
        }

        public PizzaRecord Pizza { get; set; }
        public SizeRecord Size { get; set; }
        public List<ToppingRecord> ExtraToppings { get; set; }
        public Money Total { get; set; }
    }
}