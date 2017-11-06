namespace ShoppingCart.Data.User
{
    public interface IUserRepository
    {
        GetUserResponse GetByEmail();
        SaveOrUpdateResponse Save(SaveOrUpdateRequest request);
    }
}