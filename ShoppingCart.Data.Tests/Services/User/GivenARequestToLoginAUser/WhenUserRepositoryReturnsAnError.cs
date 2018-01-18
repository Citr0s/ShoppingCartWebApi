using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Services.User;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Tests.Services.User.GivenARequestToLoginAUser
{
    [TestFixture]
    public class WhenUserRepositoryReturnsAnError
    {
        private Mock<IUserRepository> _userRepository;
        private LoginUserResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(x => x.GetByEmail(It.IsAny<string>(), It.IsAny<string>())).Returns(new GetUserResponse
            {
                HasError = true,
                Error = new Error
                {
                    Code = ErrorCodes.DatabaseError
                }
            });

            var subject = new UserService(_userRepository.Object);
            _result = subject.Login("email@address.com", "password");
        }

        [Test]
        public void ThenTheUserRepositoryIsCalledWithCorrectlyMappedEmailAddress()
        {
            _userRepository.Verify(x => x.GetByEmail(It.Is<string>(y => y == "email@address.com"), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ThenTheUserRepositoryIsCalledWithCorrectlyMappedPassword()
        {
            _userRepository.Verify(x => x.GetByEmail(It.IsAny<string>(), It.Is<string>(y => y == "password")), Times.Once);
        }

        [Test]
        public void ThenTheResponseDoesNotHaveAnError()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenCorrectErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }
    }
}