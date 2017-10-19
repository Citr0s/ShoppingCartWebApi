using System.Collections.Generic;

namespace ShoppingCart.UserSession
{
    public interface IUserSessionService
    {
        string NewUser();
        void AddItemToBasket(string userToken, BasketItem basket);
        List<BasketItem> GetBasketForUser(string userToken);
    }
}