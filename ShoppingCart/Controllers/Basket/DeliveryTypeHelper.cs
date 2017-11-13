using System;

namespace ShoppingCart.Controllers.Basket
{
    public class DeliveryTypeHelper
    {
        public DeliveryType From(string deliveryType)
        {
            if (Enum.TryParse(deliveryType, out DeliveryType parsedEnum))
                return parsedEnum;

            return DeliveryType.Unknown;
        }
    }
}