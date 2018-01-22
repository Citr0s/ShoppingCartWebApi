using System.Web.Http;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.ToppingSize;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Api.Controllers.User
{
    [RoutePrefix("api/v1/user")]
    public class UserController : ApiController
    {
        private readonly IUserSessionService _userSessionService;
        public UserController() : this(new UserSessionService(new PizzaSizeRepository(IoC.Instance().For<IDatabase>()), new ToppingSizeRepository(IoC.Instance().For<IDatabase>()), new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>())))) { }

        public UserController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_userSessionService.NewUser());
        }
    }
}