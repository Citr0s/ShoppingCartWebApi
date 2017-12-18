using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.ToppingSize
{
    public class GetToppingSizeResponse : CommunicationResponse
    {
        public GetToppingSizeResponse()
        {
            ToppingSize = new List<ToppingSizeRecord>();
        }

        public List<ToppingSizeRecord> ToppingSize { get; set; }
    }
}