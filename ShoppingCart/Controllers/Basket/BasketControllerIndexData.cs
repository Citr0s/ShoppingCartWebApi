using ShoppingCart.Services.Voucher;
using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Basket
{
    public class BasketControllerIndexData : BaseControllerData
    {
        public Services.UserSession.Basket Basket { get; set; }
        public VoucherDetailsModel Voucher { get; set; }
    }
}