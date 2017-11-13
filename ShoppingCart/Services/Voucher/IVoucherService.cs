namespace ShoppingCart.Services.Voucher
{
    public interface IVoucherService
    {
        GetAllVouchersResponse GetAll();
        GetVoucherByIdResponse GetById(int voucherId);
    }
}