using System;

namespace ShoppingCart.Data.Services.Basket
{
    public class DeliveryTypeHelper
    {
        public static DeliveryType From(string deliveryType)
        {
            if (Enum.TryParse(deliveryType, out DeliveryType parsedEnum))
                return parsedEnum;

            return DeliveryType.Unknown;
        }
    }
}