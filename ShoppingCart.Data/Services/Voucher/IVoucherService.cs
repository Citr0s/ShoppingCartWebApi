using System.Collections.Generic;
using ShoppingCart.Data.Services.Basket;

namespace ShoppingCart.Data.Services.Voucher
{
    public interface IVoucherService
    {
        GetAllVouchersResponse GetAll();
        GetVoucherByIdResponse GetById(int voucherId);
        VerifyVoucherResponse Verify(UserSession.Basket userBasket, List<DeliveryType> deliveryTypes, string voucherCode);
    }
}