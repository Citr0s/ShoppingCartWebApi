using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Basket
{
    public class BasketControllerSavedData : BaseControllerData
    {
        public Services.UserSession.Basket Basket { get; set; }
    }
}