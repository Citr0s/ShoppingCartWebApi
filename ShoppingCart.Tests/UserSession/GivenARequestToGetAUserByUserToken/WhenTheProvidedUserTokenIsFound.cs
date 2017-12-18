using System;
using NUnit.Framework;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Tests.UserSession.GivenARequestToGetAUserByUserToken
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsFound
    {
        private int _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null);
            var userToken = subject.NewUser();

            _result = subject.GetUserByUserToken(userToken);
        }

        [Test]
        public void ThenZeroIsReturned()
        {
            Assert.That(_result, Is.Zero);
        }
    }
}
