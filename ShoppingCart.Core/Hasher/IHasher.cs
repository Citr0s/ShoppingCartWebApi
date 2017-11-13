namespace ShoppingCart.Core.Hasher
{
    public interface IHasher
    {
        string Hash(string inputString);
    }
}