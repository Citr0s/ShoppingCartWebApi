using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Order;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IOrderRepository _orderRepository;

        public BasketService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public BasketCheckoutResponse Checkout(DeliveryType delivery, string voucher, string userId)
        {
            var response = new BasketCheckoutResponse();

            if (delivery == DeliveryType.Unknown)
                response.AddError(new Error { Message = "Delivery type not specified." });

            var userBasket = UserSessionService.Instance().GetBasketForUser(userId);

            if (!voucher.IsEmpty())
            {
                // TODO: logic for checking if order matches voucher criteria
            }

            var orderRequest = new SaveOrderRequest
            {
                Orders = userBasket.Items.ConvertAll(x => new Order
                {
                    PizzaId = x.Pizza.Id,
                    SizeId = x.Size.Id,
                    ExtraToppingIds = x.ExtraToppings.ConvertAll(y => y.Id),
                    SubTotal = x.Total.InPence
                }),
                Total = userBasket.Total.InPence
            };

            var saveOrderResponse = _orderRepository.SaveOrder(orderRequest);

            return response;
        }
    }
}