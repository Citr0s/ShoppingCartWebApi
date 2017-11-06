namespace ShoppingCart.Services.User
{
    public interface IUserService
    {
        CreateUserResponse Register(string email, string password);
        LoginUserResponse Login(string email, string password);
    }
}