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

            return response;
        }
    }
}