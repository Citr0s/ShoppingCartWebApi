using System.Web.WebPages;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Order;
using ShoppingCart.Services.UserSession;

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
                response.AddError(new Error { ErrorCode =  ErrorCodes.UserNotLoggedIn, Message = "You have to be logged in to complete your order" });
                return response;
            }

            if (delivery == DeliveryType.Unknown)
            {
                response.AddError(new Error {Message = "Delivery type not specified."});
                return response;
            }

            if (orderStatus == OrderStatus.Unknown)
            {
                response.AddError(new Error {Message = "Order status not specified."});
                return response;
            }

            var userBasket = _userSessionService.GetBasketForUser(userId);

            if (!voucher.IsEmpty())
            {
                // TODO: logic for checking if order matches voucher criteria
            }


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

            return response;
        }

        public BasketSaveResponse Save(string userId, OrderStatus orderStatus)
        {
            var response = new BasketSaveResponse();

            if (!_userSessionService.IsLoggedIn(userId))
            {
                response.AddError(new Error { ErrorCode = ErrorCodes.UserNotLoggedIn, Message = "You have to be logged in to save your order" });
                return response;
            }

            if (orderStatus == OrderStatus.Unknown)
            {
                response.AddError(new Error { Message = "Order status not specified." });
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

        public GetSavedORdersResponse GetSavedOrders(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}