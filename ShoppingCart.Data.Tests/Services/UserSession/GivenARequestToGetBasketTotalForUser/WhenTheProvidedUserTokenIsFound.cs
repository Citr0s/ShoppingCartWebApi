using NUnit.Framework;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Services.UserSession;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToGetBasketTotalForUser
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsFound
    {
        private Money _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            var userToken = subject.NewUser();

            _result = subject.GetBasketTotalForUser(userToken);
        }

        [Test]
        public void ThenEmptyMoneyObjectIsReturned()
        {
            Assert.That(_result.InPence, Is.Zero);
        }
    }
}