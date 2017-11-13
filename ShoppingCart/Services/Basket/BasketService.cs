using System.Collections.Generic;
using System.Web.WebPages;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Order;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;

namespace ShoppingCart.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserSessionService _userSessionService;

        public BasketService(IOrderRepository orderRepository, IUserSessionService userSessionService)
        {
            _orderRepository = orderRepository;
            _userSessionService = userSessionService;
        }

        public BasketCheckoutResponse Checkout(DeliveryType delivery, string voucher, string userId, OrderStatus orderStatus)
        {
            var response = new BasketCheckoutResponse();

            if (!_userSessionService.IsLoggedIn(userId))
            {
                response.AddError(new Error { Code =  ErrorCodes.UserNotLoggedIn, UserMessage = "You have to be logged in to complete your order" });
                return response;
            }

            if (delivery == DeliveryType.Unknown)
            {
                response.AddError(new Error {UserMessage = "Delivery type not specified."});
                return response;
            }

            if (orderStatus == OrderStatus.Unknown)
            {
                response.AddError(new Error {UserMessage = "Order status not specified."});
                return response;
            }

            var userBasket = _userSessionService.GetBasketForUser(userId);

            if (!voucher.IsEmpty())
                userBasket.Total = VoucherHelper.Check(userBasket, new List<DeliveryType>{ delivery }, voucher);
            else
                voucher = "";


            var orderRequest = new SaveOrderRequest
            {
                UserId = _userSessionService.GetUserByUserToken(userId),
                DeliveryType = delivery.ToString(),
                Voucher = voucher,
                GrandTotal = userBasket.Total.InPence,
                Status = orderStatus.ToString(),
                Orders = userBasket.Items.ConvertAll(x => new Order
                {
                    PizzaId = x.Pizza.Id,
                    SizeId = x.Size.Id,
                    ExtraToppingIds = x.ExtraToppings.ConvertAll(y => y.Id),
                    SubTotal = x.Total.InPence
                }),
            };
            var saveOrderResponse = _orderRepository.SaveOrder(orderRequest);

            if (saveOrderResponse.HasError)
                response.AddError(saveOrderResponse.Error);

            _userSessionService.ClearBasket(userId);

            return response;
        }

        public BasketSaveResponse Save(string userId, OrderStatus orderStatus)
        {
            var response = new BasketSaveResponse();

            if (!_userSessionService.IsLoggedIn(userId))
            {
                response.AddError(new Error { Code = ErrorCodes.UserNotLoggedIn, UserMessage = "You have to be logged in to save your order" });
                return response;
            }

            if (orderStatus == OrderStatus.Unknown)
            {
                response.AddError(new Error { UserMessage = "Order status not specified." });
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
                Orders = userBasket.Items.ConvertAll(x => new Order
                {
                    PizzaId = x.Pizza.Id,
                    SizeId = x.Size.Id,
                    ExtraToppingIds = x.ExtraToppings.ConvertAll(y => y.Id),
                    SubTotal = x.Total.InPence
                }),
            };
            var saveOrderResponse = _orderRepository.SaveOrder(orderRequest);

            if (saveOrderResponse.HasError)
                response.AddError(saveOrderResponse.Error);

            return response;
        }

        public GetPreviousOrdersResponse GetPreviousOrders(int userId)
        {
            var response = new GetPreviousOrdersResponse();

            var previousOrders = _orderRepository.GetPreviousOrders(userId);

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

            var previousOrders = _orderRepository.GetSavedOrders(userId);

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