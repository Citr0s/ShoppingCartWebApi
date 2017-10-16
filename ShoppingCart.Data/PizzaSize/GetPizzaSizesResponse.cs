using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.PizzaSize
{
    public class GetPizzaSizesResponse : CommunicationResponse
    {
        public GetPizzaSizesResponse()
        {
            PizzaSizes = new List<PizzaSizeRecord>();
        }

        public List<PizzaSizeRecord> PizzaSizes { get; set; }
    }
}