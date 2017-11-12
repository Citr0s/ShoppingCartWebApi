using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Order
{
    public class GetPreviousOrdersResponse : CommunicationResponse
    {
        public GetPreviousOrdersResponse()
        {
            BasketDetails = new List<BasketDetails>();    
        }

        public List<BasketDetails> BasketDetails { get; set; }
    }
}