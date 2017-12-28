using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Voucher;
using ShoppingCart.Services.Voucher;
using GetAllVouchersResponse = ShoppingCart.Data.Voucher.GetAllVouchersResponse;

namespace ShoppingCart.Tests.Services.Voucher.GivenARequestToVerifyUserBasket
{
    [TestFixture]
    public class WhenVoucherRepositoryReturnsAnError
    {
        private Mock<IVoucherRepository> _voucherRepository;
        private VerifyVoucherResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _voucherRepository = new Mock<IVoucherRepository>();
            _voucherRepository.Setup(x => x.GetAllVouchers()).Returns(() => new GetAllVouchersResponse
            {
                HasError = true,
                Error = new Error
                {
                    Code = ErrorCodes.DatabaseError
                }
            });

            var subject = new VoucherService(_voucherRepository.Object);
            _result = subject.Verify(new ShoppingCart.Services.UserSession.Basket(), new List<DeliveryType>(), "VOUCHER_CODE");
        }

        [Test]
        public void ThenVoucherRepositoryIsCalled()
        {
            _voucherRepository.Verify(x => x.GetAllVouchers(), Times.Once);
        }

        [Test]
        public void ThenTheResponseHasAnError()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenTheResponseHasCorrectErrorCode()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }
    }
}
