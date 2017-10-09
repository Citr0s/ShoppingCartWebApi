using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Topping
{
    public class GetToppingsResponse : CommunicationResponse
    {
        public GetToppingsResponse()
        {
            Toppings = new List<ToppingRecord>();
        }

        public List<ToppingRecord> Toppings { get; set; }
    }
}