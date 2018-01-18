using System.Web.Mvc;
using ShoppingCart.Core.Hasher;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.IoC;
using ShoppingCart.Data.Services.User;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.User;

namespace ShoppingCart.Controllers.User
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public RegisterController() : this(UserSessionService.Instance(),
            new UserService(new UserRepository(IoC.Instance().For<IDatabase>(), new Hasher())))
        {
        }

        public RegisterController(IUserSessionService userSessionService, IUserService userService)
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

            var response = new RegisterControllerIndexData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString()),
                LoggedIn = _userSessionService.IsLoggedIn(Session["UserId"].ToString())
            };

            return View(response);
        }

        [HttpPost]
        public ActionResult Register(string email, string password, string phone, string address)
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var response = new RegisterControllerIndexData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString())
            };

            var registerUserResponse = _userService.Register(email, password, phone, address);

            if (registerUserResponse.HasError)
            {
                response.HasError = true;
                response.Message = registerUserResponse.Error.UserMessage;
                return View("Index", response);
            }

            _userSessionService.LogIn(Session["UserId"].ToString(), registerUserResponse.UserId);
            return Redirect("/");
        }
    }
}