using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToLogAUserOut
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsFound
    {
        private UserSessionService _subject;
        private string _userToken;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = new UserSessionService(null, null, null);
            _userToken = _subject.NewUser();
            _subject.LogIn(_userToken, 1);

            _subject.LogOut(_userToken);
        }

        [Test]
        public void ThenTheUserIsLoggedOut()
        {
            Assert.That(_subject.IsLoggedIn(_userToken), Is.False);
        }
    }
}