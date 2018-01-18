using System;
using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToLogAUserOut
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsNotFound
    {
        private UserSessionService _subject;
        private string _userToken;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = new UserSessionService(null, null, null);
            _userToken = _subject.NewUser();
            _subject.LogIn(_userToken, 1);

            _subject.LogOut(Guid.NewGuid().ToString());
        }

        [Test]
        public void ThenTheUserIsNotLoggedOut()
        {
            Assert.That(_subject.IsLoggedIn(_userToken), Is.True);
        }
    }
}