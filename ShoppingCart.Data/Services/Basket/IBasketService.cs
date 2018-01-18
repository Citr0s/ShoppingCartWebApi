﻿using ShoppingCart.Data.Order;

namespace ShoppingCart.Data.Services.Basket
{
    public interface IBasketService
    {
        BasketCheckoutResponse Checkout(DeliveryType delivery, string voucher, string userId, OrderStatus orderStatus);
        BasketSaveResponse Save(string userId, OrderStatus orderStatus);
        GetPreviousOrdersResponse GetPreviousOrders(int userId);
        GetSavedOrdersResponse GetSavedOrders(int userId);
        GetBasketByIdResponse GetBasketById(int basketId);
    }
}