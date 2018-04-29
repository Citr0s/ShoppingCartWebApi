using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Api.Controllers.Deals
{
    [RoutePrefix("api/v1/deal")]
    public class DealsController : ApiController
    {
        private readonly IVoucherService _voucherService;
        private readonly IUserSessionService _userSessionService;

        public DealsController() : this(new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>())), UserSessionService.Instance()) { }

        public DealsController(IVoucherService voucherService, IUserSessionService userSessionService)
        {
            _voucherService = voucherService;
            _userSessionService = userSessionService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(VoucherDetailsMapper.Map(_voucherService.GetAll().VoucherDetails));
        }

        [HttpGet]
        [Route("{userToken}")]
        public IHttpActionResult GetSelectedDeal(string userToken)
        {
            if (Request.Headers.Authorization == null)
                return Unauthorized();

            return Ok(_userSessionService.GetVoucherForUser(Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter))));
        }

        [HttpPost]
        [Route("apply")]
        public IHttpActionResult ApplyDeal([FromBody] ApplyDealRequest request)
        {
            var getVoucherById = _voucherService.GetById(request.DealId);

            if (getVoucherById.HasError)
                return BadRequest();

            _userSessionService.SelectDeal(request.UserToken, VoucherDetailsMapper.Map(
                new List<VoucherDetails>
                {
                    new VoucherDetails
                    {
                        Voucher = getVoucherById.Voucher,
                        AllowedDeliveryTypes = getVoucherById.AllowedDeliveryTypes,
                        AllowedSizes = getVoucherById.AllowedSizes
                    }
                })[0]);

            return Ok(_userSessionService.GetVoucherForUser(request.UserToken));
        }
    }
}