using System.Collections.Generic;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Services.Size
{
    public class GetAllSizesResponse : CommunicationResponse
    {
        public GetAllSizesResponse()
        {
            Sizes = new List<SizeModel>();
        }

        public List<SizeModel> Sizes { get; set; }
    }
}