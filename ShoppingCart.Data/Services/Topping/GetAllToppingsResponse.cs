using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Services.Topping
{
    public class GetAllToppingsResponse : CommunicationResponse
    {
        public GetAllToppingsResponse()
        {
            Toppings = new List<ToppingModel>();
        }

        public List<ToppingModel> Toppings { get; set; }
    }
}