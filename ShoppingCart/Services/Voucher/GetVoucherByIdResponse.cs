using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Services.Voucher
{
    public class GetVoucherByIdResponse : CommunicationResponse
    {
        public VoucherRecord Voucher { get; set; }
    }
}