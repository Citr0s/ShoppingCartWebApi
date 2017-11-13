using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaSize;

namespace ShoppingCart.Data.Tests.PizzaPrice.GivenAGetPizzaSizeRepository
{
    [TestFixture]
    public class WhenTheDatabaseFailsToRetrieveThePizzaSizeRecords
    {
        private GetPizzaSizesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<PizzaSizeRecord>()).Throws<Exception>();

            var subject = new PizzaSizeRepository(database.Object);
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
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Something went wrong when retrieving PizzaPriceRecords from database."));
        }

        [Test]
        public void ThenAnEmptyListOfPizzaRecordsIsReturned()
        {
            Assert.That(_result.PizzaSizes.Count, Is.Zero);
        }
    }
}
