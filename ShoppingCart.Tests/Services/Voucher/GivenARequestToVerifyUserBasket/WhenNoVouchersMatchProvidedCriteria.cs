using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Delivery;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Voucher;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;
using GetAllVouchersResponse = ShoppingCart.Data.Voucher.GetAllVouchersResponse;

namespace ShoppingCart.Tests.Services.Voucher.GivenARequestToVerifyUserBasket
{
    [TestFixture]
    public class WhenNoVouchersMatchProvidedCriteria
    {
        private Mock<IVoucherRepository> _voucherRepository;
        private VerifyVoucherResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _voucherRepository = new Mock<IVoucherRepository>();
            _voucherRepository.Setup(x => x.GetAllVouchers()).Returns(() => new GetAllVouchersResponse
            {
                VoucherDetails = new List<VoucherDetails>
                {
                    new VoucherDetails
                    {
                        Voucher = new VoucherRecord
                        {
                            Code = "VOUCHER_CODE",
                            Quantity = 2,
                            Price = "1200"
                        },
                        AllowedDeliveryTypes = new List<VoucherDeliveryTypeRecord>
                        {
                            new VoucherDeliveryTypeRecord
                            {
                                DeliveryType = new DeliveryTypeRecord
                                {
                                    Name = "Collection"
                                }
                            }
                        },
                        AllowedSizes = new List<VoucherSizeRecord>
                        {
                            new VoucherSizeRecord
                            {
                                Size = new SizeRecord
                                {
                                    Name = "Small"
                                }
                            }
                        }
                    },
                    new VoucherDetails
                    {
                        Voucher = new VoucherRecord
                        {
                            Code = "VOUCHER_CODE",
                            Quantity = 2,
                            Price = "1400"
                        },
                        AllowedDeliveryTypes = new List<VoucherDeliveryTypeRecord>
                        {
                            new VoucherDeliveryTypeRecord
                            {
                                DeliveryType = new DeliveryTypeRecord
                                {
                                    Name = "Delivery"
                                }
                            }
                        },
                        AllowedSizes = new List<VoucherSizeRecord>
                        {
                            new VoucherSizeRecord
                            {
                                Size = new SizeRecord
                                {
                                    Name = "Small"
                                }
                            }
                        }
                    }
                }
            });

            var subject = new VoucherService(_voucherRepository.Object);

            var userBasket = new Basket
            {
                Items = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Size = new SizeRecord
                        {
                            Name = "Small"
                        },
                        Total = Money.From(1500)
                    }
                }
            };
            var deliveryTypes = new List<DeliveryType>
            {
                DeliveryType.Collection
            };
            _result = subject.Verify(userBasket, deliveryTypes, "VOUCHER_CODE");
        }

        [Test]
        public void ThenVoucherRepositoryIsCalledCorrectly()
        {
            _voucherRepository.Verify(x => x.GetAllVouchers(), Times.Once);
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenCorrectErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.NoMatchingVoucherFound));
        }
    }
}