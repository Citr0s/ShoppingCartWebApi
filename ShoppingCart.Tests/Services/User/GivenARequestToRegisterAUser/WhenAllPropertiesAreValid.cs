using Moq;
using NUnit.Framework;
using ShoppingCart.Data.User;
using ShoppingCart.Services.User;

namespace ShoppingCart.Tests.Services.User.GivenARequestToRegisterAUser
{
    [TestFixture]
    public class WhenAllPropertiesAreValid
    {
        private CreateUserResponse _result;
        private Mock<IUserRepository> _userRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(x => x.Save(It.IsAny<SaveUserRequest>())).Returns(() => new SaveUserResponse { UserId = 1 });

            var subject = new UserService(_userRepository.Object);
            _result = subject.Register("email@address.com", "password", "phone", "address");
        }

        [Test]
        public void ThenTheResponseDoesNotHaveAnError()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenTheUserRepositoryIsCalledWithCorrectlyMappedEmailAddress()
        {
            _userRepository.Verify(x => x.Save(It.Is<SaveUserRequest>(y => y.Email == "email@address.com")), Times.Once);
        }

        [Test]
        public void ThenTheUserRepositoryIsCalledWithCorrectlyMappedPassword()
        {
            _userRepository.Verify(x => x.Save(It.Is<SaveUserRequest>(y => y.Password == "password")), Times.Once);
        }

        [Test]
        public void ThenTheUserRepositoryIsCalledWithCorrectlyMappedPhone()
        {
            _userRepository.Verify(x => x.Save(It.Is<SaveUserRequest>(y => y.PhoneNumber == "phone")), Times.Once);
        }

        [Test]
        public void ThenTheUserRepositoryIsCalledWithCorrectlyMappedAddress()
        {
            _userRepository.Verify(x => x.Save(It.Is<SaveUserRequest>(y => y.Address == "address")), Times.Once);
        }

        [Test]
        public void ThenTheUserIdentifierIsReturnedInTheResponse()
        {
            Assert.That(_result.UserId, Is.EqualTo(1));
        }
    }
}