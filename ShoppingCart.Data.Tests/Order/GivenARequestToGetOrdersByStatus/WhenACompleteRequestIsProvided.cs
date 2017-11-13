using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Order;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Tests.Order.GivenARequestToGetOrdersByStatus
{
    [TestFixture]
    public class WhenACompleteRequestIsProvided
    {
        private GetOrdersByStatusResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<BasketRecord>()).Returns(() => new List<BasketRecord>
            {
                new BasketRecord
                {
                    User = new UserRecord
                    {
                        Id = 1
                    },
                    Status = "Complete",
                    Total = 1200
                },
                new BasketRecord
                {
                    User = new UserRecord
                    {
                        Id = 1
                    },
                    Status = "Complete",
                    Total = 1000
                },
                new BasketRecord
                {
                    User = new UserRecord
                    {
                        Id = 2
                    },
                    Status = "Complete",
                    Total = 1500
                },
                new BasketRecord
                {
                    User = new UserRecord
                    {
                        Id = 1
                    },
                    Status = "Partial",
                    Total = 1600
                }
            });
            database.Setup(x => x.Query<OrderRecord>()).Returns(() => new List<OrderRecord>());
            database.Setup(x => x.Query<OrderToppingRecord>()).Returns(() => new List<OrderToppingRecord>());

            var subject = new OrderRepository(database.Object);
            _result = subject.GetOrdersByStatus(1, OrderStatus.Complete);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenCorrectNumberOfRecordsIsReturned()
        {
            Assert.That(_result.BasketDetails.Count, Is.EqualTo(2));
        }

        [TestCase(0, 1200)]
        [TestCase(1, 1000)]
        public void ThenTheCorrectOrdersAreReturned(int index, int expected)
        {
            Assert.That(_result.BasketDetails[index].Total.InPence, Is.EqualTo(expected));
        }
    }
}