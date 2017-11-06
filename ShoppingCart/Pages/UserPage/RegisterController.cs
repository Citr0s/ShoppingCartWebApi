using System.Web.Mvc;
using ShoppingCart.Data.User;
using ShoppingCart.Services.User;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Pages.UserPage
{
    public class  RegisterController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IUserService _userService;

        public RegisterController() : this(UserSessionService.Instance(), new UserService(new UserRepository())) { }

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
        public ActionResult Register(string email, string password)
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var response = new RegisterControllerIndexData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString())
            };

            var registerUserResponse = _userService.Register(email, password);

            if (registerUserResponse.HasError)
            {
                response.HasError = true;
                response.Message = registerUserResponse.Error.Message;
                return View("Index", response);
            }

            _userSessionService.LogIn(Session["UserId"].ToString(), registerUserResponse.UserId);
            return Redirect("/");
        }
    }
}