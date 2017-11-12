using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Order;

namespace ShoppingCart.Services.Basket
{
    public class GetBasketByIdResponse : CommunicationResponse
    {
        public BasketDetails Basket { get; set; }
    }
}