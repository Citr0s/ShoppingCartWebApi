using ShoppingCart.Core.Money;

namespace ShoppingCart.Views
{
    public class BaseControllerData
    {
        public BaseControllerData()
        {
            Total = Money.From(0);
        }

        public Money Total { get; set; }
        public bool LoggedIn { get; set; }
    }
}