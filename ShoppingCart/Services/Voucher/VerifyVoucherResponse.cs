using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Money;

namespace ShoppingCart.Services.Voucher
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