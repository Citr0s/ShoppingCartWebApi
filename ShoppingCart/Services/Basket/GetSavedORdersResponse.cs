using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Money;

namespace ShoppingCart.Services.Basket
{
    public class GetSavedORdersResponse : CommunicationResponse
    {
        public UserSession.Basket Basket { get; set; }
        public Money Total { get; set; }
    }
}