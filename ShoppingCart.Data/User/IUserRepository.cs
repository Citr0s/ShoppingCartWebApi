namespace ShoppingCart.Data.User
{
    public interface IUserRepository
    {
        GetUserResponse GetByEmail();
        SaveOrUpdateResponse SaveOrUpdate(SaveOrUpdateRequest request);
    }
}