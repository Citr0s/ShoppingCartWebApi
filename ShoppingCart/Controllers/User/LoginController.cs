using System.Web.Mvc;
using ShoppingCart.Core.Hasher;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.Services.User;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.User;

namespace ShoppingCart.Controllers.User
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public LoginController() : this(UserSessionService.Instance(),
            new UserService(new UserRepository(IoC.Instance().For<IDatabase>(), new Hasher())))
        {
        }

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
                response.Message = userServiceResponse.Error.UserMessage;
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