﻿namespace ShoppingCart.Data.Services.User
{
    public interface IUserService
    {
        CreateUserResponse Register(string email, string password, string phone, string address);
        LoginUserResponse Login(string email, string password);
    }
}