using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.ToppingSize;
using ShoppingCart.Services.UserSession;

namespace ShoppingCart.Tests.UserSession.GivenARequestToAddItemToUsersBasket
{
    [TestFixture]
    public class WhenToppingSizeRepositoryReturnsAnError
    {
        private string _result;
        private UserSessionService _subject;
        private Mock<IPizzaSizeRepository> _pizzaSizeRepository;
        private Mock<IToppingSizeRepository> _toppingSizeRepository;
        private Basket _basket;

        [OneTimeSetUp]
        public void SetUp()
        {
            _pizzaSizeRepository = new Mock<IPizzaSizeRepository>();
            _pizzaSizeRepository.Setup(x => x.GetByIds(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
                new GetPizzaSizeResponse
                {
                    PizzaSize = new PizzaSizeRecord
                    {
                        Pizza = new PizzaRecord
                        {
                            Id = 1,
                            Name = "Original"
                        },
                        Size = new SizeRecord
                        {
                            Id = 2,
                            Name = "Medium"
                        },
                        Price = 1200
                    }
                });

            _toppingSizeRepository = new Mock<IToppingSizeRepository>();
            _toppingSizeRepository.Setup(x => x.GetByIds(It.IsAny<List<int>>(), It.IsAny<int>())).Returns(() =>
                new GetToppingSizeResponse
                {
                    HasError = true,
                    Error = new Error
                    {
                        UserMessage = "An Error Occurred"
                    }
                });

            _subject = new UserSessionService(_pizzaSizeRepository.Object, _toppingSizeRepository.Object);
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

        [TestCase(3)]
        [TestCase(4)]
        public void ThenToppingSizeRepositoryIsCalledWithCorrectlyMappedToppingId(int identifier)
        {
            _toppingSizeRepository.Verify(
                x => x.GetByIds(It.Is<List<int>>(y => y.Contains(identifier)), It.IsAny<int>()), Times.Once);
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
        public void ThenToppingSizeRepositoryIsCalledWithCorrectlyMappedSizeId()
        {
            _toppingSizeRepository.Verify(x => x.GetByIds(It.IsAny<List<int>>(), It.Is<int>(y => y == 2)), Times.Once);
        }

        [Test]
        public void ThenTotalDoesNotChange()
        {
            Assert.That(_basket.Total.InPence, Is.EqualTo(0));
        }
    }
}