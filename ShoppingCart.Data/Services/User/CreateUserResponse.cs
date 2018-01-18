using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Services.User
{
    public class CreateUserResponse : CommunicationResponse
    {
        public int UserId { get; set; }
    }
}