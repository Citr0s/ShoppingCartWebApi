using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Basket
{
    public class BasketControllerIndexData : BaseControllerData
    {
        public Data.Services.UserSession.Basket Basket { get; set; }
        public VoucherDetailsModel Voucher { get; set; }
    }
}