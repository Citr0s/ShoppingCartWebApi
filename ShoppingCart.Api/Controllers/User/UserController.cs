using System;
using System.Text;
using System.Web.Http;
using ShoppingCart.Core.Hasher;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.Order;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Services.Basket;
using ShoppingCart.Data.Services.User;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.ToppingSize;
using ShoppingCart.Data.User;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Api.Controllers.User
{
    [RoutePrefix("api/v1/user")]
    public class UserController : ApiController
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IUserService _userService;
        private readonly IBasketService _basketService;

        public UserController() : this(
            new UserSessionService(new PizzaSizeRepository(IoC.Instance().For<IDatabase>()),
                new ToppingSizeRepository(IoC.Instance().For<IDatabase>()),
                new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>()))),
            new UserService(new UserRepository(IoC.Instance().For<IDatabase>(), new Hasher())),
            new BasketService(new OrderRepository(IoC.Instance().For<IDatabase>()), UserSessionService.Instance(),
                new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>()))))
        {
        }

        public UserController(IUserSessionService userSessionService, IUserService userService,
            IBasketService basketService)
        {
            _userSessionService = userSessionService;
            _userService = userService;
            _basketService = basketService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult NewUser()
        {
            return Ok(_userSessionService.NewUser());
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            if (request.UserToken == null || request.Username == null || request.Password == null)
                return BadRequest();

            var userServiceResponse = _userService.Login(request.Username, request.Password);

            if (userServiceResponse.HasError)
                return Ok(userServiceResponse);

            _userSessionService.LogIn(request.UserToken, userServiceResponse.UserId);

            var loginResponse = new LoginResponse
            {
                IsLoggedIn = _userSessionService.IsLoggedIn(request.UserToken),
                UserId = userServiceResponse.UserId
            };
            return Ok(loginResponse);
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] RegisterRequest request)
        {
            if (request.UserToken == null || request.Email == null || request.Password == null ||
                request.Phone == null || request.Address == null)
                return BadRequest();

            var registerUserResponse = _userService.Register(request.Email, request.Password, request.Phone, request.Address);

            if (registerUserResponse.HasError)
                return Ok(registerUserResponse);

            _userSessionService.LogIn(request.UserToken, registerUserResponse.UserId);

            var loginResponse = new LoginResponse
            {
                IsLoggedIn = _userSessionService.IsLoggedIn(request.UserToken),
                UserId = registerUserResponse.UserId
            };
            return Ok(loginResponse);
        }

        [HttpGet]
        [Route("{userToken}/loggedIn")]
        public IHttpActionResult LoggedIn(string userToken)
        {
            if (Request.Headers.Authorization == null)
                return Unauthorized();

            return Ok(_userSessionService.IsLoggedIn(Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter))));
        }

        [HttpGet]
        [Route("{userToken}/logout")]
        public IHttpActionResult Logout(string userToken)
        {
            if (Request.Headers.Authorization == null)
                return Unauthorized();

            _userSessionService.LogOut(Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter)));
            return Ok(_userSessionService.IsLoggedIn(Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter))));
        }

        [HttpGet]
        [Route("{userId}/order/saved")]
        public IHttpActionResult GetSavedOrders(int userId)
        {
            return Ok(_basketService.GetSavedOrders(userId));
        }

        [HttpGet]
        [Route("{userId}/order/history")]
        public IHttpActionResult GetPreviousOrders(int userId)
        {
            return Ok(_basketService.GetPreviousOrders(userId));
        }

        [HttpPost]
        [Route("{userToken}/order/save")]
        public IHttpActionResult SaveOrder(string userToken)
        {
            if (Request.Headers.Authorization == null)
                return Unauthorized();

            return Ok(_basketService.Save(Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter)), OrderStatus.Partial));
        }

        [HttpPost]
        [Route("{userToken}/order/{orderId}/apply")]
        public IHttpActionResult Apply(string userToken, int orderId)
        {
            if (Request.Headers.Authorization == null)
                return Unauthorized();

            var selectedBasket = _basketService.GetBasketById(orderId);

            if (selectedBasket.HasError)
                return Ok(selectedBasket);


            var mappedBasket = new Data.Services.UserSession.Basket
            {
                Total = selectedBasket.Basket.Total,
                Items = selectedBasket.Basket.Orders.ConvertAll(orderDetails => new BasketItem
                {
                    Pizza = orderDetails.Order.Pizza,
                    Size = orderDetails.Order.Size,
                    ExtraToppings = orderDetails.Toppings.ConvertAll(x => x.Topping),
                    Total = orderDetails.Total
                })
            };

            _userSessionService.ClearBasketForUser(Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter)));
            _userSessionService.SetBasketForUser(Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter)), mappedBasket);

            return Ok();
        }
    }
}