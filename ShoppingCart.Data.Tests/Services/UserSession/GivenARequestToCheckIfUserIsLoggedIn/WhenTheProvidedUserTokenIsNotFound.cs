using System;
using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToCheckIfUserIsLoggedIn
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsNotFound
    {
        private bool _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            subject.NewUser();

            _result = subject.IsLoggedIn(Guid.NewGuid().ToString());
        }

        [Test]
        public void ThenFalseIsReturned()
        {
            Assert.That(_result, Is.False);
        }
    }
}