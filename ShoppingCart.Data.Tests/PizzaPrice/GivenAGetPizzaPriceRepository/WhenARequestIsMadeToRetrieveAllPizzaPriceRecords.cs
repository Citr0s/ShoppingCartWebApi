using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaPrice;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.Tests.PizzaPrice.GivenAGetPizzaPriceRepository
{
    [TestFixture]
    public class WhenARequestIsMadeToRetrieveAllPizzaPriceRecords
    {
        private GetPizzaPricesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<PizzaPriceRecord>()).Returns(new List<PizzaPriceRecord>
            {
                new PizzaPriceRecord
                {
                    Id = 1,
                    Pizza = new PizzaRecord
                    {
                        Id = 1,
                        Name = "Original"
                    },
                    Size = new SizeRecord
                    {
                        Id = 1,
                        Name = "Small"
                    },
                    Price = 800
                },
                new PizzaPriceRecord
                {
                    Id = 2,
                    Pizza = new PizzaRecord
                    {
                        Id = 2,
                        Name = "Gimme the Meat"
                    },
                    Size = new SizeRecord
                    {
                        Id = 2,
                        Name = "Medium"
                    },
                    Price = 1100
                }
            });

            var subject = new GetPizzaPriceRepository(database.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAllPizzaRecordsAreReturned()
        {
            Assert.That(_result.PizzaPrices.Count, Is.EqualTo(2));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenThePizzaPriceRecordIdentifierIsMappedCorrectly(int index, int identifier)
        {
            Assert.That(_result.PizzaPrices[index].Id, Is.EqualTo(identifier));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenThePizzaRecordIdentifierIsMappedCorrectly(int index, int identifier)
        {
            Assert.That(_result.PizzaPrices[index].Pizza.Id, Is.EqualTo(identifier));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenTheSizeRecordIdentifierIsMappedCorrectly(int index, int identifier)
        {
            Assert.That(_result.PizzaPrices[index].Size.Id, Is.EqualTo(identifier));
        }

        [TestCase(0, "Original")]
        [TestCase(1, "Gimme the Meat")]
        public void ThenThePizzaRecordNameIsMappedCorrectly(int index, string name)
        {
            Assert.That(_result.PizzaPrices[index].Pizza.Name, Is.EqualTo(name));
        }

        [TestCase(0, "Small")]
        [TestCase(1, "Medium")]
        public void ThenThSizeRecordNameIsMappedCorrectly(int index, string name)
        {
            Assert.That(_result.PizzaPrices[index].Size.Name, Is.EqualTo(name));
        }
    }
}
