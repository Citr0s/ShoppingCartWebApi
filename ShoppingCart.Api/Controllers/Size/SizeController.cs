using System.Web.Http;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.Services.Size;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Api.Controllers.Size
{
    [RoutePrefix("api/v1/size")]
    public class SizeController : ApiController
    {
        private readonly ISizeService _sizeService;
        public SizeController() : this(new SizeService(new SizeRepository(IoC.Instance().For<IDatabase>()))) { }

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_sizeService.GetAll());
        }
    }
}