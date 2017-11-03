using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.ToppingSize
{
    public class GetToppingSizeResponse : CommunicationResponse
    {
        public List<ToppingSizeRecord> ToppingSize { get; set; }
    }
}