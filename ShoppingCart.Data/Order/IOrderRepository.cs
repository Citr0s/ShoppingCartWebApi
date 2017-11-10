namespace ShoppingCart.Data.Order
{
    public interface IOrderRepository
    {
        SaveOrderResponse SaveOrder(SaveOrderRequest request);
    }
}