using System.Web.Mvc;
using System.Web.WebPages;
using ShoppingCart.Core.Email;
using ShoppingCart.Data.User;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Pages.UserPage
{
    public class  RegisterController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IUserRepository _userRepository;

        public RegisterController() : this(UserSessionService.Instance(), new UserRepository()) { }

        public RegisterController(IUserSessionService userSessionService, IUserRepository userRepository)
        {
            _userSessionService = userSessionService;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var response = new LoginControllerIndexData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString())
            };

            return View(response);
        }

        [HttpPost]
        public ActionResult Register(string email, string password)
        {
            if (Session["UserId"] == null)
                Session["UserId"] = _userSessionService.NewUser();

            var response = new LoginControllerIndexData
            {
                Basket = _userSessionService.GetBasketForUser(Session["UserId"].ToString()),
                Total = _userSessionService.GetBasketTotalForUser(Session["UserId"].ToString())
            };

            if (email.IsEmpty() || password.IsEmpty())
            {
                response.HasError = true;
                response.Message = "Email and password are required.";

                return View("Index", response);
            }

            if(!EmailValidator.IsValid(email))
            {
                response.HasError = true;
                response.Message = "Please provide a valid email address.";

                return View("Index", response);
            }

            var saveOrUpdateRequest = new SaveOrUpdateRequest
            {
                Email = email,
                Password = password
            };
            var saveOrUpdateResponse = _userRepository.SaveOrUpdate(saveOrUpdateRequest);

            if (saveOrUpdateResponse.HasError)
            {
                response.HasError = true;
                response.Message = "Could not create account. Please try again later.";

                return View("Index", response);
            }

            response.Message = "Account created successfully.";
            return View("Index", response);
        }
    }
}