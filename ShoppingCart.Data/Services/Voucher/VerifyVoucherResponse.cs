using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Money;

namespace ShoppingCart.Data.Services.Voucher
{
    public class VerifyVoucherResponse : CommunicationResponse
    {
        public VerifyVoucherResponse()
        {
            Total = Money.From(0);
        }

        public Money Total { get; set; }
    }
}