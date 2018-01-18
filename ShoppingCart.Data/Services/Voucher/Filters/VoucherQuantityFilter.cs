using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Data.Services.Filter;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Data.Services.Voucher.Filters
{
    public class VoucherQuantityFilter : IFilter<List<VoucherDetails>>
    {
        private readonly int _quantity;

        public VoucherQuantityFilter(int quantity)
        {
            _quantity = quantity;
        }

        public List<VoucherDetails> Execute(List<VoucherDetails> vouchers)
        {
            return vouchers.Where(x => x.Voucher.Quantity == _quantity).ToList();
        }
    }
}