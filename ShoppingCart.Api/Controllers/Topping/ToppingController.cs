using System.Web.Http;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.Services.Topping;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Api.Controllers.Topping
{
    [RoutePrefix("api/v1/topping")]
    public class ToppingController : ApiController
    {
        private readonly IToppingService _toppingService;
        public ToppingController() : this(new ToppingService(new ToppingRepository(IoC.Instance().For<IDatabase>()))) { }

        public ToppingController(IToppingService toppingService)
        {
            _toppingService = toppingService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_toppingService.GetAll());
        }
    }
}