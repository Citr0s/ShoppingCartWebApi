using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Order;
using ShoppingCart.Services.Basket;
using GetBasketByIdResponse = ShoppingCart.Services.Basket.GetBasketByIdResponse;

namespace ShoppingCart.Tests.Services.Basket.GivenARequestToGetBasketById
{
    [TestFixture]
    public class WhenValidUserIdIsProvided
    {
        private Mock<IOrderRepository> _orderRepository;
        private GetBasketByIdResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _orderRepository.Setup(x => x.GetBasketById(It.IsAny<int>())).Returns(() => new Data.Order.GetBasketByIdResponse
            {
                BasketDetails = new BasketDetails
                {
                    Total = Money.From(1200)
                }
            });

            var subject = new BasketService(_orderRepository.Object, null, null);
            _result = subject.GetBasketById(1);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenOrderRepositoryIsCalledWithCorrectlyMappedUserId()
        {
            _orderRepository.Verify(x => x.GetBasketById(1), Times.Once);
        }

        [Test]
        public void ThenBasketDetailsAreReturned()
        {
            Assert.That(_result.Basket.Total.InPence, Is.EqualTo(1200));
        }
    }
}