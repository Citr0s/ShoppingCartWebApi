namespace ShoppingCart.Data.Voucher
{
    public interface IVoucherRepository
    {
        GetAllVouchersResponse GetAllVouchers();
        GetVoucherByIdResponse GetVoucherById(int voucherId);
    }
}