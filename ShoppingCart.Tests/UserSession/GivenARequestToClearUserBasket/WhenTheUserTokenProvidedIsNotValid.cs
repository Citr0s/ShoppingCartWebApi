using System;
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

namespace ShoppingCart.Tests.UserSession.GivenARequestToClearUserBasket
{
    [TestFixture]
    public class WhenTheUserTokenProvidedIsNotValid
    {
        private Basket _result;
        private Mock<IToppingSizeRepository> _toppingSizeRepository;
        private Mock<IPizzaSizeRepository> _pizzaSizeRepository;
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
            subject.ClearBasketForUser(Guid.NewGuid().ToString());

            _result = subject.GetBasketForUser(userToken);
        }

        [Test]
        public void ThenTheCorrectItemIsStillInTheBasket()
        {
            Assert.That(_result.Items.Any(x => x.Pizza.Id == 1), Is.True);
        }

        [Test]
        public void ThenTheItemsAreNotRemovedFromTheBasket()
        {
            Assert.That(_result.Items.Count, Is.EqualTo(1));
        }
    }
}