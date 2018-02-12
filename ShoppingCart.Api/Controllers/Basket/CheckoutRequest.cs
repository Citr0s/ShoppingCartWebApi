namespace ShoppingCart.Api.Controllers.Basket
{
    public class CheckoutRequest
    {
        public string DeliveryType { get; set; }
        public string Voucher { get; set; }
    }
}