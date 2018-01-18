using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Order;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Services.Basket;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.Tests.Services.Basket.GivenARequestToCheckout
{
    [TestFixture]
    public class WhenOrderCouldNotBeSaved
    {
        private Mock<IOrderRepository> _orderRepository;
        private Mock<IUserSessionService> _userSessionService;
        private Mock<IVoucherService> _voucherService;
        private BasketCheckoutResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _orderRepository.Setup(x => x.SaveOrder(It.IsAny<SaveOrderRequest>())).Returns(() => new SaveOrderResponse
            {
                HasError = true,
                Error = new Error
                {
                    Code = ErrorCodes.DatabaseError
                }
            });

            _userSessionService = new Mock<IUserSessionService>();
            _userSessionService.Setup(x => x.IsLoggedIn(It.IsAny<string>())).Returns(() => true);
            _userSessionService.Setup(x => x.GetBasketForUser(It.IsAny<string>())).Returns(() => new Data.Services.UserSession.Basket
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
            _voucherService.Setup(x => x.Verify(It.IsAny<Data.Services.UserSession.Basket>(), It.IsAny<List<DeliveryType>>(),
                It.IsAny<string>())).Returns(() => new VerifyVoucherResponse
            {
                Total = Money.From(1100)
            });

            var subject = new BasketService(_orderRepository.Object, _userSessionService.Object, _voucherService.Object);
            _result = subject.Checkout(DeliveryType.Collection, "SOME_VOUCHER", "SOME_USER_ID",
                OrderStatus.Complete);
        }

        [Test]
        public void ThenResponseHasErrors()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenResponseHasErrorCode()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }

        [Test]
        public void ThenUserSessionServiceLoggedInIsCalledWithCorrectlyMappedUserId()
        {
            _userSessionService.Verify(x => x.IsLoggedIn("SOME_USER_ID"), Times.Once);
        }

        [Test]
        public void ThenUserSessionServiceGetBasketForUserIsCalledWithCorrectlyMappedUserId()
        {
            _userSessionService.Verify(x => x.GetBasketForUser("SOME_USER_ID"), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedUserId()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.UserId == 1)), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedDeliveryType()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.DeliveryType == DeliveryType.Collection.ToString())), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedVoucher()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.Voucher == "SOME_VOUCHER")), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedGrandTotal()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.GrandTotal == 1100)), Times.Once);
        }

        [Test]
        public void ThenOrderRepositorySaveOrderIsCalledWithCorrectlyMappedStatus()
        {
            _orderRepository.Verify(x => x.SaveOrder(It.Is<SaveOrderRequest>(y => y.Status == OrderStatus.Complete.ToString())), Times.Once);
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

        [Test]
        public void ThenVoucherServiceIsCalledCorrectly()
        {
            _voucherService.Verify(x => x.Verify(It.IsAny<Data.Services.UserSession.Basket>(), It.IsAny<List<DeliveryType>>(),
                It.IsAny<string>()), Times.Once);
        }
    }
}