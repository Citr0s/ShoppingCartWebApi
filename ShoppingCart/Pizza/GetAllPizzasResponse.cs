using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Pizza
{
    public class GetAllPizzasResponse : CommunicationResponse
    {
        public GetAllPizzasResponse()
        {
            Pizzas = new List<PizzaPriceModel>();
        }

        public List<PizzaPriceModel> Pizzas { get; set; }
    }
}