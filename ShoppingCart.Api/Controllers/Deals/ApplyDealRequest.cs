namespace ShoppingCart.Api.Controllers.Deals
{
    public class ApplyDealRequest
    {
        public int DealId { get; set; }
        public string UserToken { get; set; }
    }
}