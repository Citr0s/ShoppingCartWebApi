using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.PizzaSize;

namespace ShoppingCart.Data.Tests.PizzaSize.GivenARequestToGetAllPizzaSizes
{
    [TestFixture]
    public class WhenNoErrorsOccur
    {
        private GetPizzaSizesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<PizzaSizeRecord>()).Returns(() => new List<PizzaSizeRecord>
            {
                new PizzaSizeRecord
                {
                    Id = 1
                },
                new PizzaSizeRecord
                {
                    Id = 2
                }
            });

            var subject = new PizzaSizeRepository(database.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenCorrectNumberOfRecordsIsReturned()
        {
            Assert.That(_result.PizzaSizes.Count, Is.EqualTo(2));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenThePizzaSizeRecordsAreCorrectlyMapped(int index, int expected)
        {
            Assert.That(_result.PizzaSizes[index].Id, Is.EqualTo(expected));   
        }
    }
}
