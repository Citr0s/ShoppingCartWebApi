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
            new UserSessionService(new PizzaSizeRepository(IoC.Instance().For<IDatabase>()), new ToppingSizeRepository(IoC.Instance().For<IDatabase>()), new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>()))),
            new UserService(new UserRepository(IoC.Instance().For<IDatabase>(), new Hasher())),
            new BasketService(new OrderRepository(IoC.Instance().For<IDatabase>()), UserSessionService.Instance(), new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>()))))
            { }

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
                return Ok(userServiceResponse.Error);

            _userSessionService.LogIn(request.UserToken, userServiceResponse.UserId);

            var loginResponse = new LoginResponse
            {
                IsLoggedIn = _userSessionService.IsLoggedIn(request.UserToken),
                UserId = userServiceResponse.UserId
            };
            return Ok(loginResponse);
        }

        [HttpGet]
        [Route("{userToken}/loggedIn")]
        public IHttpActionResult LoggedIn(string userToken)
        {
            if (userToken == null)
                return BadRequest();

            return Ok(_userSessionService.IsLoggedIn(userToken));
        }

        [HttpGet]
        [Route("{userToken}/logout")]
        public IHttpActionResult Logout(string userToken)
        {
            if (userToken == null)
                return BadRequest();

            _userSessionService.LogOut(userToken);
            return Ok(_userSessionService.IsLoggedIn(userToken));
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
            return Ok(_basketService.Save(userToken, OrderStatus.Partial));
        }
    }
}