using System.Web.Http;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.Data.Services.PizzaPrice;

namespace ShoppingCart.Api.Controllers.Pizza
{
    [RoutePrefix("api/v1/pizza")]
    public class PizzaController : ApiController
    {
        private readonly IPizzaSizeService _pizzaService;

        public PizzaController() : this(new PizzaSizeService(new PizzaSizeRepository(IoC.Instance().For<IDatabase>()), new PizzaToppingRepository(IoC.Instance().For<IDatabase>()))) { }

        public PizzaController(IPizzaSizeService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_pizzaService.GetAll());
        }
    }
}