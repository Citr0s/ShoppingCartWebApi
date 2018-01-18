using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Delivery;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Voucher;
using GetAllVouchersResponse = ShoppingCart.Data.Services.Voucher.GetAllVouchersResponse;

namespace ShoppingCart.Data.Tests.Services.Voucher.GivenARequestToGetAllVouchers
{
    [TestFixture]
    public class WhenVouchersAreFoundInTheDatabase
    {
        private Mock<IVoucherRepository> _voucherRepository;
        private GetAllVouchersResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _voucherRepository = new Mock<IVoucherRepository>();
            _voucherRepository.Setup(x => x.GetAllVouchers()).Returns(() => new Data.Voucher.GetAllVouchersResponse
            {
                VoucherDetails = new List<VoucherDetails>
                {
                    new VoucherDetails
                    {
                        Voucher = new VoucherRecord
                        {
                            Id = 1,
                            Code = "SOME_VOUCHER_CODE",
                            Price = "1200",
                            Quantity = 2,
                            OnlyNamed = true,
                            Title = "AWESOME DEAL"
                        },
                        AllowedSizes = new List<VoucherSizeRecord>
                        {
                            new VoucherSizeRecord
                            {
                                Id = 1,
                                Voucher = new VoucherRecord {Id = 1},
                                Size = new SizeRecord {Id = 1}
                            }
                        },
                        AllowedDeliveryTypes = new List<VoucherDeliveryTypeRecord>
                        {
                            new VoucherDeliveryTypeRecord
                            {
                                Id = 1,
                                Voucher = new VoucherRecord {Id = 1},
                                DeliveryType = new DeliveryTypeRecord {Id = 1}
                            }
                        }
                    }
                }
            });

            var subject = new VoucherService(_voucherRepository.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenVoucherRepositoryIsCalledCorrectly()
        {
            _voucherRepository.Verify(x => x.GetAllVouchers(), Times.Once);
        }

        [Test]
        public void ThenVoucherIdentifierIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].Voucher.Id, Is.EqualTo(1));
        }

        [Test]
        public void ThenVoucherCodeIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].Voucher.Code, Is.EqualTo("SOME_VOUCHER_CODE"));
        }

        [Test]
        public void ThenVoucherPriceIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].Voucher.Price, Is.EqualTo("1200"));
        }

        [Test]
        public void ThenVoucherQuantityIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].Voucher.Quantity, Is.EqualTo(2));
        }

        [Test]
        public void ThenVoucherOnlyNamedIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].Voucher.OnlyNamed, Is.True);
        }

        [Test]
        public void ThenVoucherTitleIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].Voucher.Title, Is.EqualTo("AWESOME DEAL"));
        }

        [Test]
        public void ThenVoucherAllowedDeliveryTypesVoucherIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].AllowedDeliveryTypes[0].Voucher.Id, Is.EqualTo(1));
        }

        [Test]
        public void ThenVoucherAllowedDeliveryTypesDeliveryTypeIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].AllowedDeliveryTypes[0].DeliveryType.Id, Is.EqualTo(1));
        }

        [Test]
        public void ThenVoucherAllowedSizesVoucherIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].AllowedSizes[0].Voucher.Id, Is.EqualTo(1));
        }

        [Test]
        public void ThenVoucherAllowedSizesAllowedSizeIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.VoucherDetails[0].AllowedSizes[0].Size.Id, Is.EqualTo(1));
        }
    }
}