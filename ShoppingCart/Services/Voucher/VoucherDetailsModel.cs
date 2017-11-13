using System.Collections.Generic;

namespace ShoppingCart.Services.Voucher
{
    public class VoucherDetailsModel
    {
        public VoucherDetailsModel()
        {
            AllowedDeliveryTypes = new List<VoucherDeliveryTypeModel>();
            AllowedSizes = new List<VoucherSizeModel>();
        }

        public VoucherModel Voucher { get; set; }
        public List<VoucherDeliveryTypeModel> AllowedDeliveryTypes { get; set; }
        public List<VoucherSizeModel> AllowedSizes { get; set; }
        public string Notes { get; set; }
    }
}