using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.Tests.PizzaTopping.GivenAPizzaToppingRepository
{
    [TestFixture]
    public class WhenARequestIsMadeToRetrieveAllPizzaToppingRecords
    {
        private GetPizzaToppingResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<PizzaToppingRecord>()).Returns(new List<PizzaToppingRecord>
            {
                new PizzaToppingRecord
                {
                    Id = 1,
                    Pizza = new PizzaRecord
                    {
                        Id = 1,
                        Name = "Original"
                    },
                    Topping = new ToppingRecord
                    {
                        Id = 1,
                        Name = "Cheese"
                    }
                },
                new PizzaToppingRecord
                {
                    Id = 2,
                    Pizza = new PizzaRecord
                    {
                        Id = 2,
                        Name = "Gimme the Meat"
                    },
                    Topping = new ToppingRecord
                    {
                        Id = 2,
                        Name = "Bacon"
                    }
                }
            });

            var subject = new PizzaToppingRepository(database.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAllPizzaRecordsAreReturned()
        {
            Assert.That(_result.PizzaToppings.Count, Is.EqualTo(2));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenThePizzaPriceRecordIdentifierIsMappedCorrectly(int index, int identifier)
        {
            Assert.That(_result.PizzaToppings[index].Id, Is.EqualTo(identifier));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenThePizzaRecordIdentifierIsMappedCorrectly(int index, int identifier)
        {
            Assert.That(_result.PizzaToppings[index].Pizza.Id, Is.EqualTo(identifier));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenTheSizeRecordIdentifierIsMappedCorrectly(int index, int identifier)
        {
            Assert.That(_result.PizzaToppings[index].Topping.Id, Is.EqualTo(identifier));
        }

        [TestCase(0, "Original")]
        [TestCase(1, "Gimme the Meat")]
        public void ThenThePizzaRecordNameIsMappedCorrectly(int index, string name)
        {
            Assert.That(_result.PizzaToppings[index].Pizza.Name, Is.EqualTo(name));
        }

        [TestCase(0, "Cheese")]
        [TestCase(1, "Bacon")]
        public void ThenThSizeRecordNameIsMappedCorrectly(int index, string name)
        {
            Assert.That(_result.PizzaToppings[index].Topping.Name, Is.EqualTo(name));
        }
    }
}
