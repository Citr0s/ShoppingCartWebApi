using NUnit.Framework;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Tests.Services.UserSession.GivenARequestToCheckIfUserIsLoggedIn
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsValidAndUserIsLoggedIn
    {
        private bool _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            var userToken = subject.NewUser();
            subject.LogIn(userToken, 1);

            _result = subject.IsLoggedIn(userToken);
        }

        [Test]
        public void ThenTrueIsReturned()
        {
            Assert.That(_result, Is.True);
        }
    }
}