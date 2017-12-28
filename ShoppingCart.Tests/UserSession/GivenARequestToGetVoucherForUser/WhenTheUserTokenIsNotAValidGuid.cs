using NUnit.Framework;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;

namespace ShoppingCart.Tests.UserSession.GivenARequestToGetVoucherForUser
{
    [TestFixture]
    public class WhenTheUserTokenIsNotAValidGuid
    {
        private VoucherDetailsModel _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            _result = subject.GetVoucherForUser("NOT_A_VALID_GUID");
        }

        [Test]
        public void ThenTheVoucherIsNotReturned()
        {
            Assert.That(_result.Voucher, Is.Null);
        }
    }
}