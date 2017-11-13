namespace ShoppingCart.Data.User
{
    public interface IUserRepository
    {
        GetUserResponse GetByEmail(string email, string password);
        SaveUserResponse Save(SaveUserRequest request);
    }
}