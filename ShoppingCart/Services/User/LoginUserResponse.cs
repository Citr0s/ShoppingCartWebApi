using ShoppingCart.Core.Communication;

namespace ShoppingCart.Services.User
{
    public class LoginUserResponse : CommunicationResponse
    {
        public int UserId { get; set; }
    }
}