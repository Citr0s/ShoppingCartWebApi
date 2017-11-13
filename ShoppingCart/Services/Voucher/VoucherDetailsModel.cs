using System.Collections.Generic;
using ShoppingCart.Controllers.Basket;

namespace ShoppingCart.Services.Voucher
{
    public class VoucherDetailsModel
    {
        public VoucherDetailsModel()
        {
            AllowedDeliveryTypes = new List<DeliveryType>();
            AllowedSizes = new List<VoucherSizeModel>();
        }

        public VoucherModel Voucher { get; set; }
        public List<DeliveryType> AllowedDeliveryTypes { get; set; }
        public List<VoucherSizeModel> AllowedSizes { get; set; }
    }
}