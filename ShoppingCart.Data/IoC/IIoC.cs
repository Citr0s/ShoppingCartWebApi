namespace ShoppingCart.Data.IoC
{
    public interface IIoC
    {
        T For<T>();
        void Register<T>(IAdapter adapter);
    }
}