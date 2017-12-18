using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Order;

namespace ShoppingCart.Data.Tests.Order.GivenARequestToGetBasketById
{
    [TestFixture]
    public class WhenBasketIsFoundInTheDatabase
    {
        private GetBasketByIdResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<BasketRecord>()).Returns(() => new List<BasketRecord>
            {
                new BasketRecord {Id = 1, Total = 1000}
            });
            database.Setup(x => x.Query<OrderRecord>()).Returns(() => new List<OrderRecord>
            {
                new OrderRecord {Id = 1, Basket = new BasketRecord {Id = 1}},
                new OrderRecord {Id = 2, Basket = new BasketRecord {Id = 1}}
            });
            database.Setup(x => x.Query<OrderToppingRecord>()).Returns(() => new List<OrderToppingRecord>
            {
                new OrderToppingRecord {Id = 1, Order = new OrderRecord {Id = 1}}
            });

            var subject = new OrderRepository(database.Object);
            _result = subject.GetBasketById(1);
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenOrdersAreMappedCorrectlyOntoTheResponse(int index, int expected)
        {
            Assert.That(_result.BasketDetails.Orders[index].Order.Id, Is.EqualTo(expected));
        }

        [Test]
        public void ThenBasketRecordIsCorrectlyMappedOntoTheResponse()
        {
            Assert.That(_result.BasketDetails.Basket.Id, Is.EqualTo(1));
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenOrderToppingsAreMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.BasketDetails.Orders[0].Toppings[0].Id, Is.EqualTo(1));
        }

        [Test]
        public void ThenTotalIsMappedCorrectlyOntoTheResponse()
        {
            Assert.That(_result.BasketDetails.Total.InPence, Is.EqualTo(1000));
        }
    }
}