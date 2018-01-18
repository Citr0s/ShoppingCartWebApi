using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Controllers.Deals
{
    public class DealsController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IVoucherService _voucherService;

        [ExcludeFromCodeCoverage]
        public DealsController() : this(UserSessionService.Instance(),
            new VoucherService(new VoucherRepository(IoC.Instance().For<IDatabase>())))
        {
        }

        public DealsController(IUserSessionService userSessionService, IVoucherService voucherService)
        {
            _userSessionService = userSessionService;
            _voucherService = voucherService;
        }

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var response = new DealControllerIndexData
            {
                VoucherDetails = _userSessionService.GetVoucherForUser(Session["UserId"].ToString()),
                Vouchers = VoucherDetailsMapper.Map(_voucherService.GetAll().VoucherDetails),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
            };

            return View(response);
        }

        public ActionResult ApplyDeal(int dealId)
        {
            var getVoucherById = _voucherService.GetById(dealId);

            if (getVoucherById.HasError)
                return Redirect("/Deals");

            _userSessionService.SelectDeal(Session["UserId"].ToString(), VoucherDetailsMapper.Map(
                new List<VoucherDetails>
                {
                    new VoucherDetails
                    {
                        Voucher = getVoucherById.Voucher,
                        AllowedDeliveryTypes = getVoucherById.AllowedDeliveryTypes,
                        AllowedSizes = getVoucherById.AllowedSizes
                    }
                })[0]);
            return Redirect("/Deals");
        }
    }
}