using System;
using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToLogUserIn
{
    [TestFixture]
    public class WhenTheUserTokenIsNotFound
    {
        private bool _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            var userToken = subject.NewUser();

            subject.LogIn(Guid.NewGuid().ToString(), 1);

            _result = subject.IsLoggedIn(userToken);
        }

        [Test]
        public void ThenTheUserIsNotLoggedIn()
        {
            Assert.That(_result, Is.False);
        }
    }
}