using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Data.Voucher;
using ShoppingCart.Services.Filter;

namespace ShoppingCart.Services.Voucher.Filters
{
    public class VoucherDeliveryTypeFilter : IFilter<List<VoucherDetails>>
    {
        private readonly List<DeliveryType> _deliveryTypes;

        public VoucherDeliveryTypeFilter(List<DeliveryType> deliveryTypes)
        {
            _deliveryTypes = deliveryTypes;
        }

        public List<VoucherDetails> Execute(List<VoucherDetails> vouchers)
        {
            var results = new Dictionary<VoucherDetails, List<DeliveryType>>();
            foreach (var voucher in vouchers)
            {
                var voucherDeliveryTypes = new List<DeliveryType>();
                foreach (var allowedDeliveryType in voucher.AllowedDeliveryTypes)
                {
                    if (Enum.TryParse(allowedDeliveryType.DeliveryType.Name, out DeliveryType deliveryType))
                        voucherDeliveryTypes.Add(deliveryType);
                }
                results[voucher] = voucherDeliveryTypes;
            }

            var response = new List<VoucherDetails>();

            foreach (var deliveryType in _deliveryTypes)
                response.AddRange(results.Where(x => x.Value.Contains(deliveryType)).ToDictionary(x => x.Key, x => x.Value).Keys.ToList());

            return response;
        }
    }
}