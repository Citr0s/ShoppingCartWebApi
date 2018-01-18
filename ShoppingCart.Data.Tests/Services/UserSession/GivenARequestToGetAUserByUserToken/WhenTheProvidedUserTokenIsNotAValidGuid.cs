using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToGetAUserByUserToken
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsNotAValidGuid
    {
        private UserSessionService _subject;
        private int _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = new UserSessionService(null, null, null);
            _subject.NewUser();

            _result = _subject.GetUserByUserToken("NOT_A_VALID_GUID");
        }

        [Test]
        public void ThenZeroIsReturned()
        {
            Assert.That(_result, Is.Zero);
        }
    }
}