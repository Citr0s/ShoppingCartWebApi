using System.Collections.Generic;
using ShoppingCart.Data.Services.Filter;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Data.Services.Voucher.Filters
{
    public class VoucherPipeline : Pipeline<List<VoucherDetails>>
    {
        public override List<VoucherDetails> Process(List<VoucherDetails> input)
        {
            foreach (var filter in Filters)
                input = filter.Execute(input);

            return input;
        }
    }
}