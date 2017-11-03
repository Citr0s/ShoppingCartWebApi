using ShoppingCart.Core.Money;

namespace ShoppingCart.UserSession
{
    public interface IUserSessionService
    {
        string NewUser();
        void AddItemToBasket(string userToken, BasketData basket);
        Money GetBasketTotalForUser(string userToken);
        Basket GetBasketForUser(string userToken);
    }
}