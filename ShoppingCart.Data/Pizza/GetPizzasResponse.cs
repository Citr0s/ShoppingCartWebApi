using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Pizza
{
    public class GetPizzasResponse : CommunicationResponse
    {
        // TODO: ADD ERROR CASE TESTS TO MAKE SURE IT FAILS WHEN LIST IS NOT INITIALISED
        public GetPizzasResponse()
        {
            Pizzas = new List<PizzaRecord>();
        }

        public List<PizzaRecord> Pizzas { get; set; }
    }
}