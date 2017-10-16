using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.PizzaSize
{
    public class GetPizzaSizesResponse : CommunicationResponse
    {
        public GetPizzaSizesResponse()
        {
            PizzaPrices = new List<PizzaSizeRecord>();
        }

        public List<PizzaSizeRecord> PizzaPrices { get; set; }
    }
}