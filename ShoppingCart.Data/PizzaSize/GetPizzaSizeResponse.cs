using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.PizzaSize
{
    public class GetPizzaSizeResponse : CommunicationResponse
    {
        public PizzaSizeRecord PizzaSize { get; set; }
    }
}