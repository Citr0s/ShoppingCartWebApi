namespace ShoppingCart.Data.Services.Filter
{
    public interface IFilter<T>
    {
        T Execute(T input);
    }
}