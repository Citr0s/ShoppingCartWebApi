using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.PizzaPrice
{
    public class GetPizzaPricesResponse : CommunicationResponse
    {
        public GetPizzaPricesResponse()
        {
            PizzaPrices = new List<PizzaPriceRecord>();
        }

        public List<PizzaPriceRecord> PizzaPrices { get; set; }
    }
}