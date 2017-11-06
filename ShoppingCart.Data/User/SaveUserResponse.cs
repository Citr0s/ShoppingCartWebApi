using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.User
{
    public class SaveUserResponse : CommunicationResponse
    {
        public int UserId { get; set; }
    }
}