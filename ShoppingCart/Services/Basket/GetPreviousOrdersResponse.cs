using System.Collections.Generic;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Order;

namespace ShoppingCart.Services.Basket
{
    public class GetPreviousOrdersResponse : CommunicationResponse
    {
        public GetPreviousOrdersResponse()
        {
            BasketDetails = new List<BasketDetails>();
        }

        public List<BasketDetails> BasketDetails { get; set; }
        public Money Total { get; set; }
    }
}