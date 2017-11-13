namespace ShoppingCart.Data.Order
{
    public interface IOrderRepository
    {
        SaveOrderResponse SaveOrder(SaveOrderRequest request);
        GetOrdersByStatusResponse GetOrdersByStatus(int userId, OrderStatus orderStatus);
        GetBasketByIdResponse GetBasketById(int basketId);
    }
}