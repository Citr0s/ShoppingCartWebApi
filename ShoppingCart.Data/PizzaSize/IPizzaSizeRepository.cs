namespace ShoppingCart.Data.PizzaSize
{
    public interface IPizzaSizeRepository
    {
        GetPizzaSizesResponse GetAll();
        GetPizzaSizeResponse GetByIds(int pizzaId, int sizeId);
    }
}
