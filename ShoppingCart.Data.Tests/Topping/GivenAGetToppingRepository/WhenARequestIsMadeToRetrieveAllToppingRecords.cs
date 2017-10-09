using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.Tests.Topping.GivenAGetToppingRepository
{
    [TestFixture]
    public class WhenARequestIsMadeToRetrieveAllToppingRecords
    {
        private GetToppingsResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<ToppingRecord>()).Returns(new List<ToppingRecord>
            {
                new ToppingRecord
                {
                    Id = 1,
                    Name = "Onion"
                },
                new ToppingRecord
                {
                    Id = 2,
                    Name = "Ham"
                }
            });

            var subject = new GetToppingRepository(database.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAllSizeRecordsAreReturned()
        {
            Assert.That(_result.Toppings.Count, Is.EqualTo(2));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenTheToppingRecordIdentifierIsMappedCorrectly(int index, int identifier)
        {
            Assert.That(_result.Toppings[index].Id, Is.EqualTo(identifier));
        }

        [TestCase(0, "Onion")]
        [TestCase(1, "Ham")]
        public void ThenTheToppingRecordNameIsMappedCorrectly(int index, string name)
        {
            Assert.That(_result.Toppings[index].Name, Is.EqualTo(name));
        }
    }
}
