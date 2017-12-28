using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Data.Voucher;
using ShoppingCart.Services.Filter;

namespace ShoppingCart.Services.Voucher.Filters
{
    public class VoucherCodeFilter : IFilter<List<VoucherDetails>>
    {
        private readonly string _voucherCode;

        public VoucherCodeFilter(string voucherCode)
        {
            _voucherCode = voucherCode;
        }

        public List<VoucherDetails> Execute(List<VoucherDetails> vouchers)
        {
            return vouchers.Where(x => x.Voucher.Code == _voucherCode).ToList();
        }
    }
}