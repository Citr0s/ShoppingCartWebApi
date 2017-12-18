using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Order;

namespace ShoppingCart.Data.Tests.Order.GivenARequestToGetBasketById
{
    [TestFixture]
    public class WhenDatabaseThrowsAnException
    {
        private GetBasketByIdResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<BasketRecord>()).Throws(new Exception("Something went wrong"));

            var subject = new OrderRepository(database.Object);
            _result = subject.GetBasketById(1);
        }

        [Test]
        public void ThenTheResponseContainsAnError()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenTheResponseContainsTheCorrectErrorCode()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }
    }
}
