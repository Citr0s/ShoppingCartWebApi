using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Services.User
{
    public class LoginUserResponse : CommunicationResponse
    {
        public int UserId { get; set; }
    }
}