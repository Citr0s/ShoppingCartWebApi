using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaTopping;

namespace ShoppingCart.Data.Tests.PizzaTopping.GivenAPizzaToppingRepository
{
    [TestFixture]
    public class WhenTheDatabaseFailsToRetrieveThePizzaToppingRecords
    {
        private GetPizzaToppingResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<PizzaToppingRecord>()).Throws<Exception>();

            var subject = new PizzaToppingRepository(database.Object);
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
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Something went wrong when retrieving PizzaToppingRecords from database."));
        }

        [Test]
        public void ThenAnEmptyListOfPizzaRecordsIsReturned()
        {
            Assert.That(_result.PizzaToppings.Count, Is.Zero);
        }
    }
}
