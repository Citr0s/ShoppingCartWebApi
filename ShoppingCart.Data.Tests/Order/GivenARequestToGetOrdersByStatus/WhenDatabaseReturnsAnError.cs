using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Order;

namespace ShoppingCart.Data.Tests.Order.GivenARequestToGetOrdersByStatus
{
    [TestFixture]
    public class WhenDatabaseReturnsAnError
    {
        private GetOrdersByStatusResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<BasketRecord>()).Throws(new Exception("Something went wrong"));

            var subject = new OrderRepository(database.Object);
            _result = subject.GetOrdersByStatus(1, OrderStatus.Partial);
        }

        [Test]
        public void ThenCorrectErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }

        [Test]
        public void ThenErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }
    }
}