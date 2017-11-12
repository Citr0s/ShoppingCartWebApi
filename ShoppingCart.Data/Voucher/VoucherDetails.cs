using System.Collections.Generic;

namespace ShoppingCart.Data.Voucher
{
    public class VoucherDetails
    {
        public VoucherDetails()
        {
            AllowedDeliveryTypes = new List<VoucherDeliveryTypeRecord>();
            AllowedSizes = new List<VoucherSizeRecord>();
        }

        public VoucherRecord Voucher { get; set; }
        public List<VoucherDeliveryTypeRecord> AllowedDeliveryTypes { get; set; }
        public List<VoucherSizeRecord> AllowedSizes { get; set; }
    }
}