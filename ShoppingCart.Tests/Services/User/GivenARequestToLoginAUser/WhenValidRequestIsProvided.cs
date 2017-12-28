using Moq;
using NUnit.Framework;
using ShoppingCart.Data.User;
using ShoppingCart.Services.User;

namespace ShoppingCart.Tests.Services.User.GivenARequestToLoginAUser
{
    [TestFixture]
    public class WhenValidRequestIsProvided
    {
        private LoginUserResponse _result;
        private Mock<IUserRepository> _userRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(x => x.GetByEmail(It.IsAny<string>(), It.IsAny<string>())).Returns(new GetUserResponse { User = new UserRecord { Id = 1 } });

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
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenUserIdentifierIsReturned()
        {
            Assert.That(_result.UserId, Is.EqualTo(1));
        }
    }
}
