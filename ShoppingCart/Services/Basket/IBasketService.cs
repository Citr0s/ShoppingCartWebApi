using ShoppingCart.Controllers.Basket;
using ShoppingCart.Data.Order;

namespace ShoppingCart.Services.Basket
{
    public interface IBasketService
    {
        BasketCheckoutResponse Checkout(DeliveryType delivery, string voucher, string userId, OrderStatus orderStatus);
        BasketSaveResponse Save(string userId, OrderStatus orderStatus);
    }
}