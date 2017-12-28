namespace ShoppingCart.Services.Filter
{
    public interface IFilter<T>
    {
        T Execute(T input);
    }
}