using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;

namespace ShoppingCart.Data.Tests.Pizza.GivenAGetPizzaRepository
{
    [TestFixture]
    class WhenARequestIsMadeToRetrieveAllPizzaRecords
    {
        private List<PizzaRecord> _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Select<PizzaRecord>("pizzas")).Returns(new List<PizzaRecord>
            {
                new PizzaRecord
                {
                    Identifier = Guid.NewGuid(),
                    Name = "Original"
                },
                new PizzaRecord
                {
                    Identifier = Guid.NewGuid(),
                    Name = "Gimmie the Meat"
                },
            });

            var subject = new GetPizzaRepository(database.Object);
            _result = subject.Get();
        }

        [Test]
        public void ThenAllPizzaRecordsAreReturned()
        {
            Assert.That(_result.Count, Is.EqualTo(2));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void ThenThePizzaRecordIdentifierIsMappedCorrectly(int index)
        {
            Assert.That(_result[index].Identifier, Is.TypeOf<Guid>());
        }

        [TestCase(0, "Original")]
        [TestCase(1, "Gimmie the Meat")]
        public void ThenThePizzaRecordNameIsMappedCorrectly(int index, string name)
        {
            Assert.That(_result[index].Name, Is.EqualTo(name));
        }
    }
}
