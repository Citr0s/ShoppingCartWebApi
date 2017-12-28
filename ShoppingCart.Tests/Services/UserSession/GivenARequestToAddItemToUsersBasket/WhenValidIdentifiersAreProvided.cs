using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
using ShoppingCart.Data.ToppingSize;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;

namespace ShoppingCart.Tests.Services.UserSession.GivenARequestToAddItemToUsersBasket
{
    [TestFixture]
    public class WhenValidIdentifiersAreProvided
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
                    ToppingSize = new List<ToppingSizeRecord>
                    {
                        new ToppingSizeRecord
                        {
                            Topping = new ToppingRecord
                            {
                                Id = 3,
                                Name = "Cheese"
                            },
                            Size = new SizeRecord
                            {
                                Id = 2,
                                Name = "Medium"
                            },
                            Price = 100
                        },
                        new ToppingSizeRecord
                        {
                            Topping = new ToppingRecord
                            {
                                Id = 4,
                                Name = "Tomato Sauce"
                            },
                            Size = new SizeRecord
                            {
                                Id = 2,
                                Name = "Medium"
                            },
                            Price = 100
                        }
                    }
                });

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

        [TestCase(3)]
        [TestCase(4)]
        public void ThenToppingSizeRepositoryIsCalledWithCorrectlyMappedToppingId(int identifier)
        {
            _toppingSizeRepository.Verify(
                x => x.GetByIds(It.Is<List<int>>(y => y.Contains(identifier)), It.IsAny<int>()), Times.Once);
        }

        [TestCase(3, "Cheese")]
        [TestCase(4, "Tomato Sauce")]
        public void ThenExtraToppingsIsCorrectlyAddedUnderTheCorrectUserIdentifier(int identifier, string toppingName)
        {
            Assert.That(
                _basket.Items.Any(x =>
                    x.ExtraToppings.Any(y => y.Id == identifier) && x.ExtraToppings.Any(y => y.Name == toppingName)),
                Is.True);
        }

        [Test]
        public void ThenPizzaIsCorrectlyAddedUnderTheCorrectUserIdentifier()
        {
            Assert.That(_basket.Items.Any(x => x.Pizza.Id == 1 && x.Pizza.Name == "Original"), Is.True);
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
        public void ThenSizeIsCorrectlyAddedUnderTheCorrectUserIdentifier()
        {
            Assert.That(_basket.Items.Any(x => x.Size.Id == 2 && x.Size.Name == "Medium"), Is.True);
        }

        [Test]
        public void ThenToppingSizeRepositoryIsCalledWithCorrectlyMappedSizeId()
        {
            _toppingSizeRepository.Verify(x => x.GetByIds(It.IsAny<List<int>>(), It.Is<int>(y => y == 2)), Times.Once);
        }

        [Test]
        public void ThenTotalIsCorrectlyAddedUnderTheCorrectBasketItem()
        {
            Assert.That(_basket.Items[0].Total.InPence, Is.EqualTo(1400));
        }

        [Test]
        public void ThenTotalIsCorrectlyAddedUnderTheCorrectUserIdentifier()
        {
            Assert.That(_basket.Total.InPence, Is.EqualTo(1400));
        }
    }
}