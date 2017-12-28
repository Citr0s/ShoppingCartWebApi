using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Voucher;
using ShoppingCart.Services.Voucher;
using GetVoucherByIdResponse = ShoppingCart.Services.Voucher.GetVoucherByIdResponse;

namespace ShoppingCart.Tests.Services.Voucher.GivenARequestToGetAVoucherById
{
    [TestFixture]
    public class WhenVoucherIsFound
    {
        private Mock<IVoucherRepository> _voucherRepository;
        private GetVoucherByIdResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _voucherRepository = new Mock<IVoucherRepository>();
            _voucherRepository.Setup(x => x.GetVoucherById(It.IsAny<int>())).Returns(() => new Data.Voucher.GetVoucherByIdResponse{ Voucher = new VoucherRecord{ Id = 1 }});

            var subject = new VoucherService(_voucherRepository.Object);
            _result = subject.GetById(1);
        }

        [Test]
        public void ThenVoucherRepositoryIsCalledWithCorrectlyMappedIdentifier()
        {
            _voucherRepository.Verify(x => x.GetVoucherById(It.Is<int>(y => y == 1)), Times.Once);
        }

        [Test]
        public void ThenTheResponseCointainsTheCorrectVoucher()
        {
            Assert.That(_result.Voucher.Id, Is.EqualTo(1));
        }
    }
}