namespace ShoppingCart.Data.Order
{
    public interface IOrderRepository
    {
        SaveOrderResponse SaveOrder(SaveOrderRequest request);
        GetPreviousOrdersResponse GetPreviousOrders(int userId);
        GetPreviousOrdersResponse GetSavedOrders(int userId);
    }
}