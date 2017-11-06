using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.User
{
    public class GetUserResponse : CommunicationResponse
    {
        public UserRecord User { get; set; }
    }
}