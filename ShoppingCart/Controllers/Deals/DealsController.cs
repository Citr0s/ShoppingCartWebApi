using System.Collections.Generic;
using System.Web.Mvc;
using ShoppingCart.Services.Basket;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;
using ShoppingCart.Views;

namespace ShoppingCart.Controllers.Deals
{
    public class DealsController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IVoucherService _voucherService;

        public DealsController() : this(UserSessionService.Instance(), new VoucherService()) { }

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
                Vouchers = _voucherService.GetAll(),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
            };

            return View(response);
        }
    }

    public class DealControllerIndexData : BaseControllerData
    {
        public List<VoucherModel> Vouchers { get; set; }
    }
}