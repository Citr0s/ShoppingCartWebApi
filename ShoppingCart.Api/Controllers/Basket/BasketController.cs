using System;
using System.Web.Http;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.Order;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Services.Basket;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.ToppingSize;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Api.Controllers.Basket
{
    [RoutePrefix("api/v1/basket")]
    public class BasketController : ApiController
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IBasketService _basketService;
        public BasketController() : this(new UserSessionService(new PizzaSizeRepository(IoC.Instance().For<IDatabase>()), new ToppingSizeRepository(IoC.Instance().For<IDatabase>()), new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>()))), new BasketService(new OrderRepository(IoC.Instance().For<IDatabase>()), UserSessionService.Instance(), new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>())))) { }

        public BasketController(IUserSessionService userSessionService, IBasketService basketService)
        {
            _userSessionService = userSessionService;
            _basketService = basketService;
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddToBasket([FromBody] AddToBasketRequest request)
        {
            if (request.PizzaId == 0 || request.SizeId == 0 || request.User == null)
                return BadRequest();

            var basketItem = new BasketData
            {
                PizzaId = request.PizzaId,
                SizeId = request.SizeId,
                ExtraToppingIds = request.ToppingIds
            };

            _userSessionService.AddItemToBasket(request.User.Token, basketItem);
            return Ok(_userSessionService.GetBasketForUser(request.User.Token));
        }

        [HttpGet]
        [Route("{userToken}")]
        public IHttpActionResult GetBasket(string userToken)
        {
            if (userToken == null)
                return BadRequest();

            return Ok(_userSessionService.GetBasketForUser(userToken));
        }

        [HttpGet]
        [Route("{userToken}/total")]
        public IHttpActionResult GetBasketTotal(string userToken)
        {
            if (userToken == null)
                return BadRequest();

            return Ok(_userSessionService.GetBasketTotalForUser(userToken));
        }

        [HttpPost]
        [Route("{userToken}/checkout")]
        public IHttpActionResult Checkout(string userToken, [FromBody]CheckoutRequest request)
        {
            if (userToken == null || request.DeliveryType == null)
                return BadRequest();

            var deliveryType = DeliveryTypeHelper.From(request.DeliveryType);
            if (deliveryType == DeliveryType.Unknown)
                return BadRequest();

            var basketCheckoutResponse = _basketService.Checkout(deliveryType, request.Voucher, userToken, OrderStatus.Complete);

            if (!basketCheckoutResponse.HasError)
                return Ok();

            if (basketCheckoutResponse.Error.Code == ErrorCodes.UserNotLoggedIn)
                return Ok(basketCheckoutResponse);

            return Ok();
        }
    }
}
