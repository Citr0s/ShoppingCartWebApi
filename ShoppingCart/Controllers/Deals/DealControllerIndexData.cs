using System.Collections.Generic;
using ShoppingCart.Services.Voucher;
using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Deals
{
    public class DealControllerIndexData : BaseControllerData
    {
        public DealControllerIndexData()
        {
            Vouchers = new List<VoucherDetailsModel>();
        }

        public List<VoucherDetailsModel> Vouchers { get; set; }
        public VoucherDetailsModel VoucherDetails { get; set; }
    }
}