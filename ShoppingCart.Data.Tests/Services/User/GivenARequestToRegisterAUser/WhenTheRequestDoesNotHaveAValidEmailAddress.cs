using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Services.User;

namespace ShoppingCart.Data.Tests.Services.User.GivenARequestToRegisterAUser
{
    [TestFixture]
    public class WhenTheRequestDoesNotHaveAValidEmailAddress
    {
        private CreateUserResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserService(null);
            _result = subject.Register("not_a_valid_address", "password", "phone", "address");
        }

        [Test]
        public void ThenTheResponseHasAnError()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenTheCorrectErrorIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.EmailAddressIsNotValid));
        }
    }
}