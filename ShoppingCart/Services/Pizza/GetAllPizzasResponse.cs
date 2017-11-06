using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Services.Pizza
{
    public class GetAllPizzasResponse : CommunicationResponse
    {
        public GetAllPizzasResponse()
        {
            Pizzas = new List<PizzaModel>();
        }

        public List<PizzaModel> Pizzas { get; set; }
    }
}