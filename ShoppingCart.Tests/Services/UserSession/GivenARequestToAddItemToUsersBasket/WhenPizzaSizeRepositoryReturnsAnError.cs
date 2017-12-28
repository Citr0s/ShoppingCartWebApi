using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.ToppingSize;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;

namespace ShoppingCart.Tests.Services.UserSession.GivenARequestToAddItemToUsersBasket
{
    [TestFixture]
    public class WhenPizzaSizeRepositoryReturnsAnError
    {
        private string _result;
        private UserSessionService _subject;
        private Mock<IPizzaSizeRepository> _pizzaSizeRepository;
        private Mock<IToppingSizeRepository> _toppingSizeRepository;
        private ShoppingCart.Services.UserSession.Basket _basket;
        private Mock<IVoucherService> _voucherService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _pizzaSizeRepository = new Mock<IPizzaSizeRepository>();
            _pizzaSizeRepository.Setup(x => x.GetByIds(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
                new GetPizzaSizeResponse
                {
                    HasError = true,
                    Error = new Error
                    {
                        UserMessage = "An error has occured"
                    }
                });

            _toppingSizeRepository = new Mock<IToppingSizeRepository>();
            _voucherService = new Mock<IVoucherService>();

            _subject = new UserSessionService(_pizzaSizeRepository.Object, _toppingSizeRepository.Object, _voucherService.Object);
            _result = _subject.NewUser();

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
            _subject.AddItemToBasket(_result, basketData);
            _basket = _subject.GetBasketForUser(_result);
        }

        [Test]
        public void ThenNoItemsAreAdded()
        {
            Assert.That(_basket.Items.Count, Is.Zero);
        }

        [Test]
        public void ThenPizzaSizeRepositoryIsCalledWithCorrectlyMappedPizzaId()
        {
            _pizzaSizeRepository.Verify(x => x.GetByIds(It.Is<int>(y => y == 1), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ThenPizzaSizeRepositoryIsCalledWithCorrectlyMappedSizeId()
        {
            _pizzaSizeRepository.Verify(x => x.GetByIds(It.IsAny<int>(), It.Is<int>(y => y == 2)), Times.Once);
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