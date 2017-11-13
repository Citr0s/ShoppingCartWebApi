using ShoppingCart.Core.Money;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Services.Voucher
{
    public class VoucherRecordMapper
    {
        public static VoucherModel Map(VoucherRecord voucherRecord)
        {
            var voucherModel = new VoucherModel
            {
                Id = voucherRecord.Id,
                Code = voucherRecord.Code,
                OnlyNamed = voucherRecord.OnlyNamed,
                Quantity = voucherRecord.Quantity,
                Title = voucherRecord.Title
            };

            if (!voucherRecord.Price.Contains("^"))
                voucherModel.Price = Money.From(int.Parse(voucherRecord.Price));
            else
                voucherModel.Notes = voucherRecord.Price;

            return voucherModel;
        }
    }
}