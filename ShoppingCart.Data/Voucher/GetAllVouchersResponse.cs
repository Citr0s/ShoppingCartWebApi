using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Voucher
{
    public class GetAllVouchersResponse : CommunicationResponse
    {
        public GetAllVouchersResponse()
        {
            VoucherDetails = new List<VoucherDetails>();
        }

        public List<VoucherDetails> VoucherDetails { get; set; }
    }
}