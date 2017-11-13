using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaSize;

namespace ShoppingCart.Data.Tests.PizzaSize.GivenARequestToGetPizzaSizeById
{
    [TestFixture]
    public class WhenDatabaseThrowsAnException
    {
        private GetPizzaSizeResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<PizzaSizeRecord>()).Throws(new Exception("Something went wrong"));

            var subject = new PizzaSizeRepository(database.Object);
            _result = subject.GetByIds(1, 2);
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
