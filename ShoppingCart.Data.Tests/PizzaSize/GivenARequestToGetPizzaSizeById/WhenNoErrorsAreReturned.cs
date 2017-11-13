using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.Tests.PizzaSize.GivenARequestToGetPizzaSizeById
{
    [TestFixture]
    public class WhenNoErrorsAreReturned
    {
        private GetPizzaSizeResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<PizzaSizeRecord>()).Returns(() => new List<PizzaSizeRecord>
            {
                new PizzaSizeRecord
                {
                    Price = 1200,
                    Pizza = new PizzaRecord { Id = 1 },
                    Size = new SizeRecord { Id = 1 }
                },
                new PizzaSizeRecord
                {
                    Price = 1500,
                    Pizza = new PizzaRecord { Id = 1 },
                    Size = new SizeRecord { Id = 2 }
                }
            });

            var subject = new PizzaSizeRepository(database.Object);
            _result = subject.GetByIds(1, 2);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenTheCorrectPizzaSizeRecordIsReturned()
        {
            Assert.That(_result.PizzaSize.Price, Is.EqualTo(1500));   
        }
    }
}
