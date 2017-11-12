namespace ShoppingCart.Data.User
{
    public class SaveOrUpdateRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}