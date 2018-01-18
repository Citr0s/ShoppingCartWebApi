﻿using ShoppingCart.Views;

namespace ShoppingCart.Controllers.User
{
    public class LoginControllerIndexData : BaseControllerData
    {
        public Data.Services.UserSession.Basket Basket { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
    }
}