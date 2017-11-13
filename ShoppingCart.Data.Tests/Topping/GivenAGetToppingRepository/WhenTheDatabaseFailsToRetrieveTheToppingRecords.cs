using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.Tests.Topping.GivenAGetToppingRepository
{
    [TestFixture]
    public class WhenTheDatabaseFailsToRetrieveTheToppingRecords
    {
        private GetToppingsResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<ToppingRecord>()).Throws<Exception>();

            var subject = new ToppingRepository(database.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenAnErrorMessageIsReturned()
        {
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Something went wrong when retrieving ToppingRecord from database."));
        }

        [Test]
        public void ThenAnEmptyListOfToppingRecordsIsReturned()
        {
            Assert.That(_result.Toppings.Count, Is.Zero);
        }
    }
}
