using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Services.User;

namespace ShoppingCart.Tests.Services.User.GivenARequestToLoginAUser
{
    [TestFixture]
    public class WhenEmailAddressIsNotProvided
    {
        private LoginUserResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserService(null);
            _result = subject.Login("", "password");
        }

        [Test]
        public void ThenTheResponseHasAnError()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenCorrectErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.CredentialsAreIncomplete));
        }
    }
}
