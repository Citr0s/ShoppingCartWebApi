using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Basket
{
    public class BasketControllerSummaryData : BaseControllerData
    {
        public Services.UserSession.Basket Basket { get; set; }
    }
}