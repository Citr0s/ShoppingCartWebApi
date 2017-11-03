using System.Collections.Generic;
using ShoppingCart.Core.Money;

namespace ShoppingCart.UserSession
{
    public interface IUserSessionService
    {
        string NewUser();
        void AddItemToBasket(string userToken, BasketData basket);
        Money GetBasketTotalForUser(string userToken);
        List<BasketItem> GetBasketForUser(string userToken);
    }
}