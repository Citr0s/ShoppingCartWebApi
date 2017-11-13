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
    }
}