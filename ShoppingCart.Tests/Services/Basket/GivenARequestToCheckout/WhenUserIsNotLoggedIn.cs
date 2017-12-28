using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Order;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
using ShoppingCart.Services.Basket;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;

namespace ShoppingCart.Tests.Services.Basket.GivenARequestToCheckout
{
    [TestFixture]
    public class WhenUserIsNotLoggedIn
    {
        private Mock<IOrderRepository> _orderRepository;
        private Mock<IUserSessionService> _userSessionService;
        private Mock<IVoucherService> _voucherService;
        private BasketCheckoutResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _orderRepository.Setup(x => x.SaveOrder(It.IsAny<SaveOrderRequest>())).Returns(() => new SaveOrderResponse());

            _userSessionService = new Mock<IUserSessionService>();
            _userSessionService.Setup(x => x.IsLoggedIn(It.IsAny<string>())).Returns(() => false);
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
            _result = subject.Checkout(DeliveryType.Collection, "SOME_VOUCHER", "SOME_USER_ID",
                OrderStatus.Complete);
        }

        [Test]
        public void ThenUserSessionServiceLoggedInIsCalledWithCorrectlyMappedUserId()
        {
            _userSessionService.Verify(x => x.IsLoggedIn("SOME_USER_ID"), Times.Once);
        }

        [Test]
        public void ThenResponseHasNoErrors()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenResponseContainsTheCorrectErrorCode()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.UserNotLoggedIn));
        }
    }
}