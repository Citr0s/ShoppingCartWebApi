using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Order;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
using ShoppingCart.Services.Basket;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;

namespace ShoppingCart.Tests.Services.Basket.GivenARequestToSave
{
    [TestFixture]
    public class WhenValidRequestIsSupplied
    {
        private Mock<IOrderRepository> _orderRepository;
        private Mock<IUserSessionService> _userSessionService;
        private Mock<IVoucherService> _voucherService;
        private BasketSaveResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _orderRepository.Setup(x => x.SaveOrder(It.IsAny<SaveOrderRequest>())).Returns(() => new SaveOrderResponse());

            _userSessionService = new Mock<IUserSessionService>();
            _userSessionService.Setup(x => x.IsLoggedIn(It.IsAny<string>())).Returns(() => true);
            _userSessionService.Setup(x => x.GetBasketForUser(It.IsAny<string>())).Returns(() => new ShoppingCart.Services.UserSession.Basket
            {
                Items = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Pizza = new PizzaRecord{ Id = 2 },
                        Size = new SizeRecord{ Id = 3 },
                        ExtraToppings = new List<ToppingRecord>{ new ToppingRecord {  Id = 4 } },
                        Total = Money.From(1200)
                    }
                },
                Total = Money.From(1200)
            });
            _userSessionService.Setup(x => x.GetUserByUserToken(It.IsAny<string>())).Returns(() => 1);

            _voucherService = new Mock<IVoucherService>();
            _voucherService.Setup(x => x.Verify(It.IsAny<ShoppingCart.Services.UserSession.Basket>(), It.IsAny<List<DeliveryType>>(),
                It.IsAny<string>())).Returns(() => new VerifyVoucherResponse
                {
                    Total = Money.From(1100)
                });

            var subject = new BasketService(_orderRepository.Object, _userSessionService.Object, _voucherService.Object);
            _result = subject.Save("USER_TOKEN", OrderStatus.Partial);
        }

        [Test]
        public void ThenResponseHasNoErrors()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedUserId()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.UserId == 1)), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedDeliveryType()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.DeliveryType == DeliveryType.Unknown.ToString())), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedStatus()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.Status == OrderStatus.Partial.ToString())), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedOrderPizzaId()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.Orders[0].PizzaId == 2)), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedOrderSizeId()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.Orders[0].SizeId == 3)), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedOrderExtraToppings()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.Orders[0].ExtraToppingIds[0] == 4)), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedOrderSubtotal()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.Orders[0].SubTotal == 1200)), Times.Once);
        }
    }
}