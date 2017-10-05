using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Pizza
{
    public class GetPizzasResponse : CommunicationResponse
    {
        public GetPizzasResponse()
        {
            Pizzas = new List<PizzaRecord>();
        }

        public List<PizzaRecord> Pizzas { get; set; }
    }
}