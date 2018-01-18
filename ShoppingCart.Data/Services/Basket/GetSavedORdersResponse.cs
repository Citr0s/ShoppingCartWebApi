using System.Collections.Generic;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Order;

namespace ShoppingCart.Data.Services.Basket
{
    public class GetSavedOrdersResponse : CommunicationResponse
    {
        public GetSavedOrdersResponse()
        {
            BasketDetails = new List<BasketDetails>();
        }

        public List<BasketDetails> BasketDetails { get; set; }
        public Money Total { get; set; }
    }
}