using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Order
{
    public class GetOrdersByStatusResponse : CommunicationResponse
    {
        public GetOrdersByStatusResponse()
        {
            BasketDetails = new List<BasketDetails>();
        }

        public List<BasketDetails> BasketDetails { get; set; }
    }
}