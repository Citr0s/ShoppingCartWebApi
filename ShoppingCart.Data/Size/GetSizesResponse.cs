using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Data.Size
{
    public class GetSizesResponse : CommunicationResponse
    {
        public GetSizesResponse()
        {
            Sizes = new List<SizeRecord>();
        }

        public List<SizeRecord> Sizes { get; set; }
    }
}