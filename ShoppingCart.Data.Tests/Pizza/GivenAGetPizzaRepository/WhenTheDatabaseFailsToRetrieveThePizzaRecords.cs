using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;

namespace ShoppingCart.Data.Tests.Pizza.GivenAGetPizzaRepository
{
    [TestFixture]
    public class WhenTheDatabaseFailsToRetrieveThePizzaRecords
    {
        private GetPizzasResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<PizzaRecord>()).Throws<Exception>();

            var subject = new PizzaRepository(database.Object);
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
            Assert.That(_result.Error.Message, Is.EqualTo("Something went wrong when retrieving PizzaRecords from database."));
        }

        [Test]
        public void ThenAnEmptyListOfPizzaRecordsIsReturned()
        {
            Assert.That(_result.Pizzas.Count, Is.Zero);
        }
    }
}
