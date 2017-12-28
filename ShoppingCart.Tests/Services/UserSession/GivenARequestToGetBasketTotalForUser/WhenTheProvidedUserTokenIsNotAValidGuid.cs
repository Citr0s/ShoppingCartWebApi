using NUnit.Framework;
using ShoppingCart.Core.Money;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Tests.Services.UserSession.GivenARequestToGetBasketTotalForUser
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsNotAValidGuid
    {
        private Money _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            _result = subject.GetBasketTotalForUser("NOT_A_VALID_GUID");
        }

        [Test]
        public void ThenEmptyMoneyObjectIsReturned()
        {
            Assert.That(_result.InPence, Is.Zero);
        }
    }
}