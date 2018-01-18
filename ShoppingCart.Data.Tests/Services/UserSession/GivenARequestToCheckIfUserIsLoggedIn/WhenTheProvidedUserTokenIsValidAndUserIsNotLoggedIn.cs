using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToCheckIfUserIsLoggedIn
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsValidAndUserIsNotLoggedIn
    {
        private bool _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            var userToken = subject.NewUser();

            _result = subject.IsLoggedIn(userToken);
        }

        [Test]
        public void ThenFalseIsReturned()
        {
            Assert.That(_result, Is.False);
        }
    }
}