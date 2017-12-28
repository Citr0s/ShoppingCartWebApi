using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Services.User;

namespace ShoppingCart.Tests.Services.User.GivenARequestToRegisterAUser
{
    [TestFixture]
    public class WhenTheRequestDoesNotHaveAnEmailAddress
    {
        private CreateUserResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserService(null);
            _result = subject.Register("", "password", "phone", "address");
        }

        [Test]
        public void ThenTheResponseHasAnError()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenTheCorrectErrorIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.CredentialsAreIncomplete));
        }
    }
}
