using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Order;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Tests.Order.GivenARequestToSaveAnOrder
{
    [TestFixture]
    public class WhenDatabaseThrowsAnException
    {
        private SaveOrderResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<UserRecord>()).Throws(new Exception("Something went wrong"));

            var subject = new OrderRepository(database.Object);
            _result = subject.SaveOrder(new SaveOrderRequest());
        }

        [Test]
        public void ThenTheResponseContainsAnError()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenTheCorrectErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }
    }
}