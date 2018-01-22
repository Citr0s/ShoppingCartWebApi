using System.Collections.Generic;

namespace ShoppingCart.Api.Controllers.Basket
{
    public class AddToBasketRequest
    {
        public AddToBasketRequest()
        {
            ExtraToppings = new List<int>();
        }

        public Core.Account.User User { get; set; }
        public int PizzaId { get; set; }
        public int SizeId { get; set; }
        public List<int> ExtraToppings { get; set; }
    }
}