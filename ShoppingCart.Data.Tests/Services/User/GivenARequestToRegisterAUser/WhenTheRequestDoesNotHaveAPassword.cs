using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Services.User;

namespace ShoppingCart.Data.Tests.Services.User.GivenARequestToRegisterAUser
{
    [TestFixture]
    public class WhenTheRequestDoesNotHaveAPassword
    {
        private CreateUserResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserService(null);
            _result = subject.Register("email@address.com", "", "phone", "address");
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