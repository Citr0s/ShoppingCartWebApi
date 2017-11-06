using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Services.PizzaPrice
{
    public class GetAllPizzaSizesResponse : CommunicationResponse
    {
        public GetAllPizzaSizesResponse()
        {
            Pizzas = new List<PizzaSizeModel>();
        }

        public List<PizzaSizeModel> Pizzas { get; set; }
    }
}