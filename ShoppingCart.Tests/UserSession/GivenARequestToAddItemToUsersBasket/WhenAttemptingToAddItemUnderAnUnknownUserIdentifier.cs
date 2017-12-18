using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.ToppingSize;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Tests.UserSession.GivenARequestToAddItemToUsersBasket
{
    [TestFixture]
    public class WhenAttemptingToAddItemUnderAnUnknownUserIdentifier
    {
        private UserSessionService _subject;
        private Mock<IPizzaSizeRepository> _pizzaSizeRepository;
        private Mock<IToppingSizeRepository> _toppingSizeRepository;
        private Basket _basket;

        [OneTimeSetUp]
        public void SetUp()
        {
            _pizzaSizeRepository = new Mock<IPizzaSizeRepository>();
            _toppingSizeRepository = new Mock<IToppingSizeRepository>();

            _subject = new UserSessionService(_pizzaSizeRepository.Object, _toppingSizeRepository.Object);

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
            _subject.AddItemToBasket("UNKNOWN IDENTIFIER", basketData);
            _basket = _subject.GetBasketForUser("UNKNOWN IDENTIFIER");
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