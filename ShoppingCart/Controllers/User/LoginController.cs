using System.Web.Mvc;
using ShoppingCart.Data.User;
using ShoppingCart.Services.User;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Controllers.User
{
    public class LoginController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IUserService _userService;

        public LoginController() : this(UserSessionService.Instance(), new UserService(new UserRepository())) { }

        public LoginController(IUserSessionService userSessionService, IUserService userService)
        {
            _userSessionService = userSessionService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            if (_userSessionService.IsLoggedIn(Session["UserId"].ToString()))
                return Redirect("/");

                var response = new LoginControllerIndexData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
            };

            return View(response);
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var userServiceResponse = _userService.Login(email, password);

            var response = new LoginControllerIndexData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
            };

            if (userServiceResponse.HasError)
            {
                response.HasError = true;
                response.Message = userServiceResponse.Error.Message;
                return View("Index", response);
            }

            _userSessionService.LogIn(Session["UserId"].ToString(), userServiceResponse.UserId);
            return Redirect("Index");
        }

        public ActionResult Logout()
        {
            _userSessionService.LogOut(Session["UserId"].ToString());
            return Redirect("/");
        }
    }
}