using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Order
{
    public class GetBasketByIdResponse : CommunicationResponse
    {
        public BasketDetails BasketDetails { get; set; }
    }
}