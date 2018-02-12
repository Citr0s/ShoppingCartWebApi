using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Data.Services.Filter;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Data.Services.Voucher.Filters
{
    public class VoucherSizeFilter : IFilter<List<VoucherDetails>>
    {
        private readonly List<BasketItem> _basketItems;

        public VoucherSizeFilter(List<BasketItem> basketItems)
        {
            _basketItems = basketItems;
        }

        public List<VoucherDetails> Execute(List<VoucherDetails> vouchers)
        {
            var response = new List<VoucherDetails>();
            foreach (var voucher in vouchers)
            {
                var allItemsAreValid = true;
                foreach (var basketItem in _basketItems)
                {
                    if (voucher.AllowedSizes.All(x => x.Size.Name != basketItem.Size.Name))
                        allItemsAreValid = false;
                }

                if(allItemsAreValid)
                    response.Add(voucher);
            }
            return response;
        }
    }
}