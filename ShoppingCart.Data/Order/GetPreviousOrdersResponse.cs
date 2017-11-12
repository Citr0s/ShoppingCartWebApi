using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Order
{
    public class GetPreviousOrdersResponse : CommunicationResponse
    {
        public GetPreviousOrdersResponse()
        {
            Baskets = new List<BasketRecord>();
        }

        public List<BasketRecord> Baskets { get; set; }
    }
}