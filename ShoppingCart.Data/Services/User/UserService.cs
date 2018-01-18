using FluentNHibernate.Conventions;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Core.Email;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CreateUserResponse Register(string email, string password, string phone, string address)
        {
            var response = new CreateUserResponse();

            if (email.IsEmpty() || password.IsEmpty())
            {
                response.AddError(new Error {Code = ErrorCodes.CredentialsAreIncomplete, UserMessage = "Email and password are required."});
                return response;
            }

            if (!EmailValidator.IsValid(email))
            {
                response.AddError(new Error {Code = ErrorCodes.EmailAddressIsNotValid, UserMessage = "Please provide a valid email address."});
                return response;
            }

            var saveOrUpdateRequest = new SaveUserRequest
            {
                Email = email,
                Password = password,
                PhoneNumber = phone,
                Address = address
            };
            var saveOrUpdateResponse = _userRepository.Save(saveOrUpdateRequest);

            if (saveOrUpdateResponse.HasError)
            {
                response.AddError(saveOrUpdateResponse.Error);
                return response;
            }

            response.UserId = saveOrUpdateResponse.UserId;

            return response;
        }

        public LoginUserResponse Login(string email, string password)
        {
            var response = new LoginUserResponse();

            if (email.IsEmpty() || password.IsEmpty())
            {
                response.AddError(new Error {Code = ErrorCodes.CredentialsAreIncomplete, UserMessage = "Email and password are required."});
                return response;
            }

            var saveOrUpdateResponse = _userRepository.GetByEmail(email, password);

            if (saveOrUpdateResponse.HasError)
            {
                response.AddError(saveOrUpdateResponse.Error);
                return response;
            }

            response.UserId = saveOrUpdateResponse.User.Id;

            return response;
        }
    }
}