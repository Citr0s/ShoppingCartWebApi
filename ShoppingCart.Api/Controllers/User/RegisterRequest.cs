namespace ShoppingCart.Api.Controllers.User
{
    public class RegisterRequest
    {
        public string UserToken { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}