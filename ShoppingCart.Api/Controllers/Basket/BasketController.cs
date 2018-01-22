using System.Web.Http;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.PizzaSize;
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
        public BasketController() : this(new UserSessionService(new PizzaSizeRepository(IoC.Instance().For<IDatabase>()), new ToppingSizeRepository(IoC.Instance().For<IDatabase>()), new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>())))) { }

        public BasketController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult GetAll([FromBody] AddToBasketRequest request)
        {
            if (request.PizzaId == 0 || request.SizeId == 0 || request.User == null)
                return BadRequest();

            var basketItem = new BasketData
            {
                PizzaId = request.PizzaId,
                SizeId = request.SizeId,
                ExtraToppingIds = request.ExtraToppings
            };

            _userSessionService.AddItemToBasket(request.User.Token, basketItem);
            return Ok(_userSessionService.GetBasketForUser(request.User.Token));
        }
    }
}
