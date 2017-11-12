using System.Collections.Generic;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Order;
using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Basket
{
    public class BasketControllerSavedData : BaseControllerData
    {
        public BasketControllerSavedData()
        {
            BasketDetails = new List<BasketDetails>();
        }

        public List<BasketDetails> BasketDetails { get; set; }
    }
}