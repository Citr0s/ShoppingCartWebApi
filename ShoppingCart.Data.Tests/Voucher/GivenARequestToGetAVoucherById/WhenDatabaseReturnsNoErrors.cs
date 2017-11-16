using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Delivery;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Voucher;

namespace ShoppingCart.Data.Tests.Voucher.GivenARequestToGetAVoucherById
{
    [TestFixture]
    public class WhenDatabaseReturnsNoErrors
    {
        private GetVoucherByIdResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<VoucherRecord>()).Returns(() => new List<VoucherRecord>
            {
                new VoucherRecord
                {
                    Id = 1,
                    Title = "GREAT DEAL!",
                    Price = "1200",
                    Code = "SOME_VOUCHER_CODE",
                    Quantity = 2,
                    OnlyNamed = true

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
                    Voucher = new VoucherRecord{ Id = 1 },
                    DeliveryType = new DeliveryTypeRecord{ Id = 1, Name = "Collection" }
                },
                new VoucherDeliveryTypeRecord
                {
                    Id = 4,
                    Voucher = new VoucherRecord{ Id = 1 }
                }
            });
            database.Setup(x => x.Query<VoucherSizeRecord>()).Returns(() => new List<VoucherSizeRecord>
            {
                new VoucherSizeRecord
                {
                    Id = 5,
                    Voucher = new VoucherRecord{ Id = 1 },
                    Size = new SizeRecord{ Id = 1 }
                },
                new VoucherSizeRecord
                {
                    Id = 6,
                    Voucher = new VoucherRecord{ Id = 1 }
                }
            });

            var subject = new VoucherRepository(database.Object);
            _result = subject.GetVoucherById(1);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenTheCorrectVoucherRecordIsReturned()
        {
            Assert.That(_result.Voucher.Id, Is.EqualTo(1));
        }

        [Test]
        public void ThenTheCodeIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.Voucher.Code, Is.EqualTo("SOME_VOUCHER_CODE"));
        }

        [Test]
        public void ThenTheOnlyNamedFlagIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.Voucher.OnlyNamed, Is.True);
        }

        [Test]
        public void ThenThePriceIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.Voucher.Price, Is.EqualTo("1200"));
        }

        [Test]
        public void ThenThePizzaQuantityIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.Voucher.Quantity, Is.EqualTo(2));
        }

        [Test]
        public void ThenTheTitleIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.Voucher.Title, Is.EqualTo("GREAT DEAL!"));
        }

        [TestCase(0, 3)]
        [TestCase(1, 4)]
        public void ThenTheCorrectAllowedDeliveryRecordsAreReturned(int index, int expected)
        {
            Assert.That(_result.AllowedDeliveryTypes[index].Id, Is.EqualTo(expected));
        }

        [Test]
        public void ThenTheCorrectAllowedDeliveryTypeIdIsReturned()
        {
            Assert.That(_result.AllowedDeliveryTypes.First().DeliveryType.Id, Is.EqualTo(1));
        }

        [Test]
        public void ThenTheCorrectAllowedDeliveryTypeNameIsReturned()
        {
            Assert.That(_result.AllowedDeliveryTypes.First().DeliveryType.Name, Is.EqualTo("Collection"));
        }

        [TestCase(0, 5)]
        [TestCase(1, 6)]
        public void ThenTheCorrectAllowedSizeRecordsAreReturned(int index, int expected)
        {
            Assert.That(_result.AllowedSizes[index].Id, Is.EqualTo(expected));
        }

        [Test]
        public void ThenTheCorrectAllowedSizeIdAreReturned()
        {
            Assert.That(_result.AllowedSizes.First().Size.Id, Is.EqualTo(1));
        }
    }
}
