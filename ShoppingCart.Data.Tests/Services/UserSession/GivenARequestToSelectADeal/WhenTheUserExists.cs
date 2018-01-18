using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToSelectADeal
{
    [TestFixture]
    public class WhenTheUserExists
    {
        private VoucherDetailsModel _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            var userToken = subject.NewUser();
            subject.SelectDeal(userToken,
                new VoucherDetailsModel {Voucher = new VoucherModel {Code = "SOME_VOUCHER_CODE"}});

            _result = subject.GetVoucherForUser(userToken);
        }

        [Test]
        public void ThenTheCorrectVoucherIsReturned()
        {
            Assert.That(_result.Voucher.Code, Is.EqualTo("SOME_VOUCHER_CODE"));
        }
    }
}