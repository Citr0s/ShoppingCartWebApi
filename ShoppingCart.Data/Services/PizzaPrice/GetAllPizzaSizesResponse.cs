using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Services.PizzaPrice
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