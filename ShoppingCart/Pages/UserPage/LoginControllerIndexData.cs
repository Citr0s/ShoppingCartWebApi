using ShoppingCart.Core.Money;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Pages.UserPage
{
    public class LoginControllerIndexData 
    {
        public Basket Basket { get; set; }
        public Money Total { get; set; }
    }
}