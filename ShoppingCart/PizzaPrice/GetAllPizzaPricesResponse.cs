using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.PizzaPrice
{
    public class GetAllPizzaPricesResponse : CommunicationResponse
    {
        public GetAllPizzaPricesResponse()
        {
            PizzaPrices = new List<PizzaPriceModel>();
        }

        public List<PizzaPriceModel> PizzaPrices { get; set; }
    }
}