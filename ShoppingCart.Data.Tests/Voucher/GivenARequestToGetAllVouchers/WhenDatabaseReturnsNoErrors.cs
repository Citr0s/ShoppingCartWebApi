using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Data.Tests.Voucher.GivenARequestToGetAllVouchers
{
    [TestFixture]
    public class WhenDatabaseReturnsNoErrors
    {
        private GetAllVouchersResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<VoucherRecord>()).Returns(() => new List<VoucherRecord>
            {
                new VoucherRecord
                {
                    Id = 1
                },
                new VoucherRecord
                {
                    Id = 2
                }
            });
            database.Setup(x => x.Query<VoucherDeliveryTypeRecord>()).Returns(() => new List<VoucherDeliveryTypeRecord>
            {
                new VoucherDeliveryTypeRecord
                {
                    Id = 3,
                    Voucher = new VoucherRecord {Id = 1}
                },
                new VoucherDeliveryTypeRecord
                {
                    Id = 4,
                    Voucher = new VoucherRecord {Id = 1}
                }
            });
            database.Setup(x => x.Query<VoucherSizeRecord>()).Returns(() => new List<VoucherSizeRecord>
            {
                new VoucherSizeRecord
                {
                    Id = 5,
                    Voucher = new VoucherRecord {Id = 1}
                },
                new VoucherSizeRecord
                {
                    Id = 6,
                    Voucher = new VoucherRecord {Id = 1}
                }
            });

            var subject = new VoucherRepository(database.Object);
            _result = subject.GetAllVouchers();
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenTheVoucherRecordIsMappedCorrectly(int index, int expected)
        {
            Assert.That(_result.VoucherDetails[index].Voucher.Id, Is.EqualTo(expected));
        }

        [TestCase(0, 3)]
        [TestCase(1, 4)]
        public void ThenTheCorrectAllowedDeliveryRecordsAreReturned(int index, int expected)
        {
            Assert.That(_result.VoucherDetails.First().AllowedDeliveryTypes[index].Id, Is.EqualTo(expected));
        }

        [TestCase(0, 5)]
        [TestCase(1, 6)]
        public void ThenTheCorrectAllowedSizeRecordsAreReturned(int index, int expected)
        {
            Assert.That(_result.VoucherDetails.First().AllowedSizes[index].Id, Is.EqualTo(expected));
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenTheCorrectNumberOfVouchersIsReturned()
        {
            Assert.That(_result.VoucherDetails.Count, Is.EqualTo(2));
        }
    }
}