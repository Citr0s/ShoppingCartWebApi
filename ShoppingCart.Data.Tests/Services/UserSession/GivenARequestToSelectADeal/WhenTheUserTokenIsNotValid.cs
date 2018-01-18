﻿using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToSelectADeal
{
    [TestFixture]
    public class WhenTheUserTokenIsNotValid
    {
        private VoucherDetailsModel _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new UserSessionService(null, null, null);
            var userToken = subject.NewUser();
            subject.SelectDeal("SOME_INVALID_USER_TOKEN",
                new VoucherDetailsModel {Voucher = new VoucherModel {Code = "SOME_VOUCHER_CODE"}});
            _result = subject.GetVoucherForUser(userToken);
        }

        [Test]
        public void ThenTheVoucherIsNotAssigned()
        {
            Assert.That(_result, Is.Null);
        }
    }
}