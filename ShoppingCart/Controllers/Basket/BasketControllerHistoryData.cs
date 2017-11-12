using System.Collections.Generic;
using ShoppingCart.Data.Order;
using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Basket
{
    public class BasketControllerHistoryData : BaseControllerData
    {
        public BasketControllerHistoryData()
        {
            BasketDetails = new List<BasketDetails>();
        }

        public List<BasketDetails> BasketDetails { get; set; }
    }
}