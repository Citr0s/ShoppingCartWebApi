using NUnit.Framework;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Tests.Services.UserSession.GivenARequestToLogAUserOut
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsNotValid
    {
        private UserSessionService _subject;
        private string _userToken;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = new UserSessionService(null, null, null);
            _userToken = _subject.NewUser();
            _subject.LogIn(_userToken, 1);

            _subject.LogOut("NOT_A_VALID_GUID");
        }

        [Test]
        public void ThenTheUserIsNotLoggedOut()
        {
            Assert.That(_subject.IsLoggedIn(_userToken), Is.True);
        }
    }
}