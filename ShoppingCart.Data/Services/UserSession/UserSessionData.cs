using ShoppingCart.Data.Services.Voucher;

namespace ShoppingCart.Data.Services.UserSession
{
    public class UserSessionData
    {
        public UserSessionData()
        {
            Basket = new Basket();
        }

        public int UserId { get; set; }
        public bool LoggedIn { get; set; }
        public Basket Basket { get; set; }
        public VoucherDetailsModel SelectedDeal { get; set; }

        public void LogIn(int userId)
        {
            LoggedIn = true;
            UserId = userId;
        }

        public void LogOut()
        {
            LoggedIn = false;
            UserId = 0;
        }
    }
}