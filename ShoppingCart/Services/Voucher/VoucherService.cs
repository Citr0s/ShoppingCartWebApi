using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Voucher;
using ShoppingCart.Services.Voucher.Filters;

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

            var voucherPipeline = new VoucherPipeline();

            voucherPipeline
                .Register(new VoucherCodeFilter(voucherCode))
                .Register(new VoucherQuantityFilter(userBasket.Items.Count))
                .Register(new VoucherDeliveryTypeFilter(deliveryTypes))
                .Register(new VoucherSizeFilter(userBasket.Items));

            var vouchers = voucherPipeline.Process(getAllVouchersResponse.VoucherDetails);

            if (vouchers.Count == 0)
            {
                response.AddError(new Error { UserMessage = "No vouchers matched provided criteria" });
                return response;
            }

            var finalVoucher = vouchers.First();

            if (int.TryParse(finalVoucher.Voucher.Price, out var price))
            {
                response.Total = Money.From(price);
                return response;
            }

            var topQuantity = Regex.Match(finalVoucher.Voucher.Price, "[0-9]?").Value;

            if (!int.TryParse(topQuantity, out var quantity))
                return response;

            response.Total = Money.From(userBasket.Items.Take(quantity).Sum(x => x.Total.InPence));
            return response;
        }
    }
}