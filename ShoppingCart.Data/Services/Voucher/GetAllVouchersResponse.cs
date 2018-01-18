using System.Collections.Generic;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Data.Services.Voucher
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