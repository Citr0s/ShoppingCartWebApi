namespace ShoppingCart.Api.Controllers.User
{
    public class LoginRequest
    {
        public string UserToken { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}