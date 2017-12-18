using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Data.Tests.Voucher.GivenARequestToGetAllVouchers
{
    [TestFixture]
    public class WhenDatabaseThrowsAnException
    {
        private GetAllVouchersResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<VoucherRecord>()).Throws(new Exception("Something went wrong"));

            var subject = new VoucherRepository(database.Object);
            _result = subject.GetAllVouchers();
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenTheErrorContainsTheCorrectErrorCode()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }
    }
}
