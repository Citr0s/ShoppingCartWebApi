namespace ShoppingCart.Data.User
{
    public interface IUserRepository
    {
        GetUserResponse GetByEmail();
        SaveUserResponse Save(SaveOrUpdateRequest request);
    }
}