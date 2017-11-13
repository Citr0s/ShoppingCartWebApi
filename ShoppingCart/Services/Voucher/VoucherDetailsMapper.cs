using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Services.Voucher
{
    public class VoucherDetailsMapper
    {
        public static List<VoucherDetailsModel> Map(List<VoucherDetails> voucherDetails)
        {
            var response = new List<VoucherDetailsModel>();

            foreach (var voucherDetail in voucherDetails)
            {
                var voucherDetailsModel = new VoucherDetailsModel
                {
                    Voucher = new VoucherModel
                    {
                        Id = voucherDetail.Voucher.Id,
                        Code = voucherDetail.Voucher.Code,
                        Title = voucherDetail.Voucher.Title,
                        OnlyNamed = voucherDetail.Voucher.OnlyNamed,
                        Quantity = voucherDetail.Voucher.Quantity
                    }
                };

                if (!voucherDetail.Voucher.Price.Contains("^"))
                    voucherDetailsModel.Voucher.Price = Money.From(int.Parse(voucherDetail.Voucher.Price));
                else
                {
                    var pizzaCount = int.Parse(voucherDetail.Voucher.Price.Split('^')[0]);
                    voucherDetailsModel.Notes = $"Price of {pizzaCount} most expensive pizza{(pizzaCount > 1 ? "s" : "")}.";
                }

                response.Add(voucherDetailsModel);
            }

            return response;
        }
    }
}