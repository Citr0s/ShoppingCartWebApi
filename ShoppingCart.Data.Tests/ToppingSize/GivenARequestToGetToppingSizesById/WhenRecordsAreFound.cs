using System.Collections.Generic;
using Moq;
using NHibernate.Criterion;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
using ShoppingCart.Data.ToppingSize;

namespace ShoppingCart.Data.Tests.ToppingSize.GivenARequestToGetToppingSizesById
{
    [TestFixture]
    public class WhenRecordsAreFound
    {
        private GetToppingSizeResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<ToppingSizeRecord>()).Returns(() => new List<ToppingSizeRecord>
            {
                new ToppingSizeRecord
                {
                    Id = 1,
                    Topping = new ToppingRecord { Id = 2 },
                    Size = new SizeRecord{ Id = 2 }
                },
                new ToppingSizeRecord
                {
                    Id = 2,
                    Topping = new ToppingRecord { Id = 3 },
                    Size = new SizeRecord{ Id = 2 }
                }
            });

            var subject = new ToppingSizeRepository(database.Object);
            _result = subject.GetByIds(new List<int> {2, 3}, 2);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenTheCorrectNumberOfRecordsIsReturned()
        {
            Assert.That(_result.ToppingSize.Count, Is.EqualTo(2));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThneTheCorrectRecordsAreReturned(int index, int expected)
        {
            Assert.That(_result.ToppingSize[index].Id, Is.EqualTo(expected));
        }
    }
}