using ShoppingCart.Core.Money;
using ShoppingCart.Data.Services.Voucher;

namespace ShoppingCart.Data.Services.UserSession
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
        void ClearBasketForUser(string userToken);
        void SetBasketForUser(string userToken, Basket basket);
        void SelectDeal(string userToken, VoucherDetailsModel voucher);
        VoucherDetailsModel GetVoucherForUser(string userToken);
    }
}