using ShoppingCart.Controllers.Basket;

namespace ShoppingCart.Services.Basket
{
    public interface IBasketService
    {
        BasketCheckoutResponse Checkout(DeliveryType delivery, string voucher, string userId);
    }
}