using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Order;
using ShoppingCart.Services.Basket;

namespace ShoppingCart.Tests.Services.Basket.GivenARequestToGetPreviousOrders
{
    [TestFixture]
    public class WhenValidUserIdIsProvided
    {
        private Mock<IOrderRepository> _orderRepository;
        private GetPreviousOrdersResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _orderRepository.Setup(x => x.GetOrdersByStatus(It.IsAny<int>(), It.IsAny<OrderStatus>())).Returns(() => new GetOrdersByStatusResponse
            {
                BasketDetails = new List<BasketDetails>
                {
                    new BasketDetails
                    {
                        Total = Money.From(1200)
                    }
                }
            });

            var subject = new BasketService(_orderRepository.Object, null, null);
            _result = subject.GetPreviousOrders(1);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenOrderRepositoryIsCalledWithCorrectlyMappedUserId()
        {
            _orderRepository.Verify(x => x.GetOrdersByStatus(1, It.IsAny<OrderStatus>()), Times.Once);
        }

        [Test]
        public void ThenOrderRepositoryIsCalledWithCorrectlyMappedOrderStatus()
        {
            _orderRepository.Verify(x => x.GetOrdersByStatus(It.IsAny<int>(), It.Is<OrderStatus>(y => y == OrderStatus.Complete)), Times.Once);
        }

        [Test]
        public void ThenBasketDetailsAreReturned()
        {
            Assert.That(_result.BasketDetails[0].Total.InPence, Is.EqualTo(1200));
        }
    }
}