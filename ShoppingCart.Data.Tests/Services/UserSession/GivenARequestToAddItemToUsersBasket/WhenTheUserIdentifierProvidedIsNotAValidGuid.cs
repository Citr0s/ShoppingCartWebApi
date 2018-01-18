using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.ToppingSize;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToAddItemToUsersBasket
{
    [TestFixture]
    public class WhenTheUserIdentifierProvidedIsNotAValidGuid
    {
        private UserSessionService _subject;
        private Mock<IPizzaSizeRepository> _pizzaSizeRepository;
        private Mock<IToppingSizeRepository> _toppingSizeRepository;
        private Data.Services.UserSession.Basket _basket;
        private Mock<IVoucherService> _voucherService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _pizzaSizeRepository = new Mock<IPizzaSizeRepository>();
            _toppingSizeRepository = new Mock<IToppingSizeRepository>();
            _voucherService = new Mock<IVoucherService>();

            _subject = new UserSessionService(_pizzaSizeRepository.Object, _toppingSizeRepository.Object, _voucherService.Object);
            _subject.NewUser();

            var basketData = new BasketData
            {
                PizzaId = 1,
                SizeId = 2,
                ExtraToppingIds = new List<int>
                {
                    3,
                    4
                }
            };
            _subject.AddItemToBasket("NOT A VALID GUID", basketData);
            _basket = _subject.GetBasketForUser("NOT A VALID GUID");
        }

        [Test]
        public void ThenNoItemsAreAdded()
        {
            Assert.That(_basket.Items.Count, Is.Zero);
        }

        [Test]
        public void ThenPizzaSizeRepositoryIsNeverCalled()
        {
            _pizzaSizeRepository.Verify(x => x.GetByIds(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void ThenToppingSizeRepositoryIsNeverCalled()
        {
            _toppingSizeRepository.Verify(x => x.GetByIds(It.IsAny<List<int>>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void ThenTotalDoesNotChange()
        {
            Assert.That(_basket.Total.InPence, Is.EqualTo(0));
        }
    }
}