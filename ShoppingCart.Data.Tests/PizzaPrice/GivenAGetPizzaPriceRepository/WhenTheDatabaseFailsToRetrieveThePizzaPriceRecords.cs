using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaPrice;

namespace ShoppingCart.Data.Tests.PizzaPrice.GivenAGetPizzaPriceRepository
{
    [TestFixture]
    public class WhenTheDatabaseFailsToRetrieveThePizzaPriceRecords
    {
        private GetPizzaPricesResponse _result;

        [SetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<PizzaPriceRecord>()).Throws<Exception>();

            var subject = new PizzaPriceRepository(database.Object);
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
            Assert.That(_result.Error.Message, Is.EqualTo("Something went wrong when retrieving PizzaPriceRecords from database."));
        }

        [Test]
        public void ThenAnEmptyListOfPizzaRecordsIsReturned()
        {
            Assert.That(_result.PizzaPrices.Count, Is.Zero);
        }
    }
}
