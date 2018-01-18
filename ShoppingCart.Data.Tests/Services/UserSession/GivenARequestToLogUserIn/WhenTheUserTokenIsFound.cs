using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToLogUserIn
{
    [TestFixture]
    public class WhenTheUserTokenIsFound
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
        public void ThenTheUserIsNotLoggedIn()
        {
            Assert.That(_result, Is.True);
        }
    }
}