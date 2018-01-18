using System.Collections.Generic;
using ShoppingCart.Data.Services.Basket;

namespace ShoppingCart.Data.Services.Voucher
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