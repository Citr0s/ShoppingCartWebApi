using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Voucher
{
    public class GetVoucherByIdResponse : CommunicationResponse
    {
        public VoucherRecord Voucher { get; set; }
    }
}