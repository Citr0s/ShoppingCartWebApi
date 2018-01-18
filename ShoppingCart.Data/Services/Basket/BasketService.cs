using System.Collections.Generic;
using FluentNHibernate.Conventions;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Order;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;

namespace ShoppingCart.Data.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserSessionService _userSessionService;
        private readonly IVoucherService _voucherService;

        public BasketService(IOrderRepository orderRepository, IUserSessionService userSessionService,
            IVoucherService voucherService)
        {
            _orderRepository = orderRepository;
            _userSessionService = userSessionService;
            _voucherService = voucherService;
        }

        public BasketCheckoutResponse Checkout(DeliveryType delivery, string voucher, string userId,
            OrderStatus orderStatus)
        {
            var response = new BasketCheckoutResponse();

            if (!_userSessionService.IsLoggedIn(userId))
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.UserNotLoggedIn,
                    UserMessage = "You have to be logged in to complete your order"
                });
                return response;
            }

            if (delivery == DeliveryType.Unknown)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.DeliveryTypeUnknown,
                    UserMessage = "Delivery type not specified."
                });
                return response;
            }

            if (orderStatus == OrderStatus.Unknown)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.OrderStatusUnkown,
                    UserMessage = "Order status not specified."
                });
                return response;
            }

            var userBasket = _userSessionService.GetBasketForUser(userId);

            if (!voucher.IsEmpty())
            {
                var verifyVoucherResponse =
                    _voucherService.Verify(userBasket, new List<DeliveryType> {delivery}, voucher);

                if (!verifyVoucherResponse.HasError)
                    userBasket.Total = verifyVoucherResponse.Total;
            }

            var orderRequest = new SaveOrderRequest
            {
                UserId = _userSessionService.GetUserByUserToken(userId),
                DeliveryType = delivery.ToString(),
                Voucher = voucher,
                GrandTotal = userBasket.Total.InPence,
                Status = orderStatus.ToString(),
                Orders = userBasket.Items.ConvertAll(x => new Order.Order
                {
                    PizzaId = x.Pizza.Id,
                    SizeId = x.Size.Id,
                    ExtraToppingIds = x.ExtraToppings.ConvertAll(y => y.Id),
                    SubTotal = x.Total.InPence
                })
            };
            var saveOrderResponse = _orderRepository.SaveOrder(orderRequest);

            if (saveOrderResponse.HasError)
                response.AddError(saveOrderResponse.Error);

            _userSessionService.ClearBasketForUser(userId);

            return response;
        }

        public BasketSaveResponse Save(string userId, OrderStatus orderStatus)
        {
            var response = new BasketSaveResponse();

            if (!_userSessionService.IsLoggedIn(userId))
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.UserNotLoggedIn,
                    UserMessage = "You have to be logged in to save your order"
                });
                return response;
            }

            if (orderStatus == OrderStatus.Unknown)
            {
                response.AddError(new Error {UserMessage = "Order status not specified."});
                return response;
            }

            var userBasket = _userSessionService.GetBasketForUser(userId);

            var orderRequest = new SaveOrderRequest
            {
                DeliveryType = DeliveryType.Unknown.ToString(),
                Voucher = "",
                UserId = _userSessionService.GetUserByUserToken(userId),
                GrandTotal = userBasket.Total.InPence,
                Status = orderStatus.ToString(),
                Orders = userBasket.Items.ConvertAll(x => new Order.Order
                {
                    PizzaId = x.Pizza.Id,
                    SizeId = x.Size.Id,
                    ExtraToppingIds = x.ExtraToppings.ConvertAll(y => y.Id),
                    SubTotal = x.Total.InPence
                })
            };
            var saveOrderResponse = _orderRepository.SaveOrder(orderRequest);

            if (saveOrderResponse.HasError)
                response.AddError(saveOrderResponse.Error);

            return response;
        }

        public GetPreviousOrdersResponse GetPreviousOrders(int userId)
        {
            var response = new GetPreviousOrdersResponse();

            var previousOrders = _orderRepository.GetOrdersByStatus(userId, OrderStatus.Complete);

            if (previousOrders.HasError)
            {
                response.AddError(previousOrders.Error);
                return response;
            }

            response.BasketDetails = previousOrders.BasketDetails;
            return response;
        }

        public GetSavedOrdersResponse GetSavedOrders(int userId)
        {
            var response = new GetSavedOrdersResponse();

            var previousOrders = _orderRepository.GetOrdersByStatus(userId, OrderStatus.Partial);

            if (previousOrders.HasError)
            {
                response.AddError(previousOrders.Error);
                return response;
            }

            response.BasketDetails = previousOrders.BasketDetails;
            return response;
        }

        public GetBasketByIdResponse GetBasketById(int basketId)
        {
            var response = new GetBasketByIdResponse();

            var getBasketByIdResponse = _orderRepository.GetBasketById(basketId);

            if (getBasketByIdResponse.HasError)
            {
                response.AddError(getBasketByIdResponse.Error);
                return response;
            }

            response.Basket = getBasketByIdResponse.BasketDetails;
            return response;
        }
    }
}