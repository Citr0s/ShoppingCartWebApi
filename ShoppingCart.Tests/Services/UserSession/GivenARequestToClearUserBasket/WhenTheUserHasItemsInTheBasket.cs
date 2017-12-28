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

namespace ShoppingCart.Tests.Services.UserSession.GivenARequestToClearUserBasket
{
    [TestFixture]
    public class WhenTheUserHasItemsInTheBasket
    {
        private Basket _result;
        private Mock<IPizzaSizeRepository> _pizzaSizeRepository;
        private Mock<IToppingSizeRepository> _toppingSizeRepository;
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
                        Pizza = new PizzaRecord {Id = 1},
                        Size = new SizeRecord {Id = 2}
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
                            Topping = new ToppingRecord {Id = 3},
                            Size = new SizeRecord {Id = 2}
                        }
                    }
                });

            _voucherService = new Mock<IVoucherService>();

            var subject = new UserSessionService(_pizzaSizeRepository.Object, _toppingSizeRepository.Object, _voucherService.Object);
            var userToken = subject.NewUser();
            subject.AddItemToBasket(userToken,
                new BasketData {PizzaId = 1, SizeId = 2, ExtraToppingIds = new List<int> {3}});
            subject.ClearBasketForUser(userToken);

            _result = subject.GetBasketForUser(userToken);
        }

        [Test]
        public void ThenTheItemIsRemovedFromTheBasket()
        {
            Assert.That(_result.Items.Count, Is.Zero);
        }

        [Test]
        public void ThenThePizzaSizeRepositoryIsCalledWithCorrectlyMappedPizzaId()
        {
            _pizzaSizeRepository.Verify(x => x.GetByIds(It.Is<int>(y => y == 1), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ThenThePizzaSizeRepositoryIsCalledWithCorrectlyMappedSizeId()
        {
            _pizzaSizeRepository.Verify(x => x.GetByIds(It.IsAny<int>(), It.Is<int>(y => y == 2)), Times.Once);
        }

        [Test]
        public void ThenToppingSizeRepositoryIsCalledWithCorrectlyMappedSizeId()
        {
            _toppingSizeRepository.Verify(x => x.GetByIds(It.IsAny<List<int>>(), It.Is<int>(y => y == 2)), Times.Once);
            ;
        }

        [Test]
        public void ThenToppingSizeRepositoryIsCalledWithCorrectlyMappedToppingId()
        {
            _toppingSizeRepository.Verify(x => x.GetByIds(It.Is<List<int>>(y => y.Any(z => z == 3)), It.IsAny<int>()),
                Times.Once);
            ;
        }
    }
}