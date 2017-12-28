using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Services.Voucher
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public GetAllVouchersResponse GetAll()
        {
            var response = new GetAllVouchersResponse();

            var getAllVouchersResponse = _voucherRepository.GetAllVouchers();

            if (getAllVouchersResponse.HasError)
            {
                response.AddError(getAllVouchersResponse.Error);
                return response;
            }

            response.VoucherDetails = getAllVouchersResponse.VoucherDetails;
            return response;
        }

        public GetVoucherByIdResponse GetById(int voucherId)
        {
            var response = new GetVoucherByIdResponse();

            var getVoucherByIdResponse = _voucherRepository.GetVoucherById(voucherId);

            if (getVoucherByIdResponse.HasError)
            {
                response.AddError(getVoucherByIdResponse.Error);
                return response;
            }

            response.Voucher = getVoucherByIdResponse.Voucher;
            response.AllowedDeliveryTypes = getVoucherByIdResponse.AllowedDeliveryTypes;
            response.AllowedSizes = getVoucherByIdResponse.AllowedSizes;
            return response;
        }

        public VerifyVoucherResponse Verify(UserSession.Basket userBasket, List<DeliveryType> deliveryTypes, string voucherCode)
        {
            var response = new VerifyVoucherResponse { Total = userBasket.Total };

            var getAllVouchersResponse = _voucherRepository.GetAllVouchers();

            if (getAllVouchersResponse.HasError)
            {
                response.AddError(getAllVouchersResponse.Error);
                return response;
            }

            var voucher = getAllVouchersResponse.VoucherDetails.FirstOrDefault(x => x.Voucher.Code == voucherCode && x.Voucher.Quantity == userBasket.Items.Count);

            if (voucher == null)
            {
                response.AddError(new Error { UserMessage = $"Voucher with the code of '{voucherCode}' does not exist." });
                return response;
            }

            var matchFound = false;
            foreach (var allowedDeliveryType in voucher.AllowedDeliveryTypes)
            {
                if (Enum.TryParse(allowedDeliveryType.DeliveryType.Name, out DeliveryType deliveryType))
                {
                    if (deliveryTypes.Contains(deliveryType))
                        matchFound = true;

                }
            }

            if (!matchFound)
            {
                response.AddError(new Error { UserMessage = $"Voucher does not apply for selected delivery type." });
                return response;
            }

            foreach (var basketItem in userBasket.Items)
            {
                if (voucher.AllowedSizes.All(x => x.Size.Name != basketItem.Size.Name))
                    continue;

                if (int.TryParse(voucher.Voucher.Price, out var price))
                {
                    response.Total = Money.From(price);
                    return response;
                }

                var topQuantity = Regex.Match(voucher.Voucher.Price, "[0-9]?").Value;

                if (!int.TryParse(topQuantity, out var quantity))
                    continue;

                response.Total = Money.From(userBasket.Items.Take(quantity).Sum(x => x.Total.InPence));
                return response;
            }

            return response;
        }
    }
}