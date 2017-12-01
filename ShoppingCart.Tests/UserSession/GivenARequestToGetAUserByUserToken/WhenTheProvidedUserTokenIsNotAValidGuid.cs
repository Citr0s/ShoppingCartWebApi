using System;
using NUnit.Framework;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Tests.UserSession.GivenARequestToGetAUserByUserToken
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsNotAValidGuid
    {
        private UserSessionService _subject;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = new UserSessionService(null, null);
            _subject.NewUser();
        }

        [Test]
        public void ThenZeroIsReturned()
        {
            Assert.Throws<Exception>(() => { _subject.GetUserByUserToken("NOT_A_VALID_GUID"); });
        }
    }
}
