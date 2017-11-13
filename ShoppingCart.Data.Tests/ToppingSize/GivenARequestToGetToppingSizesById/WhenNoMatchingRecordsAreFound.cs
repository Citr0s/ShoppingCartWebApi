using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.ToppingSize;

namespace ShoppingCart.Data.Tests.ToppingSize.GivenARequestToGetToppingSizesById
{
    [TestFixture]
    public class WhenNoMatchingRecordsAreFound
    {
        private GetToppingSizeResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<ToppingSizeRecord>()).Returns(() => new List<ToppingSizeRecord>());

            var subject = new ToppingSizeRepository(database.Object);
            _result = subject.GetByIds(new List<int> {1, 2}, 1);
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenTheCorrectErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.RecordNotFound));
        }
    }
}