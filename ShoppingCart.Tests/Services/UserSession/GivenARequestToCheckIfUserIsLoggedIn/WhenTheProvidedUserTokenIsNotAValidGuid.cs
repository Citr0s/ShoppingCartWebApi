using NUnit.Framework;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Tests.Services.UserSession.GivenARequestToCheckIfUserIsLoggedIn
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsNotAValidGuid
    {
        private bool _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            _result = subject.IsLoggedIn("NOT_A_VALID_GUID");
        }

        [Test]
        public void ThenFalseIsReturned()
        {
            Assert.That(_result, Is.False);
        }
    }
}