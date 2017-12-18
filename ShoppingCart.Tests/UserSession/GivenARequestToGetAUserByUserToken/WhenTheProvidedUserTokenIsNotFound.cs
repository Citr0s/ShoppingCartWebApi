using System;
using NUnit.Framework;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Tests.UserSession.GivenARequestToGetAUserByUserToken
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsNotFound
    {
        private UserSessionService _subject;
        private int _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = new UserSessionService(null, null);
            _subject.NewUser();

            _result = _subject.GetUserByUserToken(Guid.NewGuid().ToString());
        }

        [Test]
        public void ThenZeroIsReturned()
        {
            Assert.That(_result, Is.Zero);
        }
    }
}
