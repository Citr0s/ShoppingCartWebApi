using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCart.Data.Voucher;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;
using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Deals
{
    public class DealsController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IVoucherService _voucherService;

        public DealsController() : this(UserSessionService.Instance(), new VoucherService(new VoucherRepository())) { }

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

            _userSessionService.SelectDeal(Session["UserId"].ToString(), VoucherDetailsMapper.Map(new List<VoucherDetails>
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

    public class DealControllerIndexData : BaseControllerData
    {
        public List<VoucherDetailsModel> Vouchers { get; set; }
    }
}