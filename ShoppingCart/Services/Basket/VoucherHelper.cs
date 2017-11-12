using System.Linq;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Money;

namespace ShoppingCart.Services.Basket
{
    public class VoucherHelper
    {
        public static Money Check(UserSession.Basket userBasket, DeliveryType deliveryType, string voucher)
        {
            if (voucher.ToUpper() == "2FOR1TUE" && userBasket.Items.Count == 2 && userBasket.Items.All(x => x.Size.Name == "Medium"))
                return userBasket.Items.OrderByDescending(x => x.Total.InPence).ToList().First().Total;

            if (voucher.ToUpper() == "3FOR2THUR" && userBasket.Items.Count == 3 && userBasket.Items.All(x => x.Size.Name == "Medium"))
            {
                var sortedOrders = userBasket.Items.OrderByDescending(x => x.Total.InPence).ToList();
                return Money.From(sortedOrders[0].Total.InPence + sortedOrders[1].Total.InPence);
            }

            if (voucher.ToUpper() == "FAMFRIDAYCOLL" && userBasket.Items.Count == 4 && userBasket.Items.All(x => x.Size.Name == "Medium") && userBasket.Items.All(x => x.Pizza.Name.ToLower() != "create your own") && deliveryType == DeliveryType.Collection)
                return Money.From(30000);

            if (voucher.ToUpper() == "2LARGECOLL" && userBasket.Items.Count == 2 && userBasket.Items.All(x => x.Size.Name == "Large") && userBasket.Items.All(x => x.Pizza.Name.ToLower() != "create your own") && deliveryType == DeliveryType.Collection)
                return Money.From(25000);

            if (voucher.ToUpper() == "2MEDIUMCOLL" && userBasket.Items.Count == 2 && userBasket.Items.All(x => x.Size.Name == "Medium") && userBasket.Items.All(x => x.Pizza.Name.ToLower() != "create your own") && deliveryType == DeliveryType.Collection)
                return Money.From(18000);

            if (voucher.ToUpper() == "2SMALLCOLL" && userBasket.Items.Count == 2 && userBasket.Items.All(x => x.Size.Name == "Small") && userBasket.Items.All(x => x.Pizza.Name.ToLower() != "create your own") && deliveryType == DeliveryType.Collection)
                return Money.From(12000);

            return userBasket.Total;
        }
    }
}