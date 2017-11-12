using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Basket
{
    public class BasketControllerHistoryData : BaseControllerData
    {
        public Services.UserSession.Basket Basket { get; set; }
    }
}