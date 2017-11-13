using ShoppingCart.Core.Money;
using ShoppingCart.Services.Voucher;

namespace ShoppingCart.Services.UserSession
{
    public interface IUserSessionService
    {
        string NewUser();
        void AddItemToBasket(string userToken, BasketData basket);
        Money GetBasketTotalForUser(string userToken);
        Basket GetBasketForUser(string userToken);
        void LogIn(string userToken, int userId);
        bool IsLoggedIn(string toString);
        void LogOut(string toString);
        int GetUserByUserToken(string userToken);
        void ClearBasket(string userToken);
        void SetBasket(string userToken, Basket basket);
        void SelectDeal(string userToken, VoucherDetailsModel voucher);
        VoucherDetailsModel GetVoucherForUser(string userToken);
    }
}