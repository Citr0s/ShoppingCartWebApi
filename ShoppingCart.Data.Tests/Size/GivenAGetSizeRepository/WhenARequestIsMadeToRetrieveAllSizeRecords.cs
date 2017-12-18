using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.Tests.Size.GivenAGetSizeRepository
{
    [TestFixture]
    public class WhenARequestIsMadeToRetrieveAllSizeRecords
    {
        private GetSizesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<SizeRecord>()).Returns(new List<SizeRecord>
            {
                new SizeRecord
                {
                    Id = 1,
                    Name = "Small"
                },
                new SizeRecord
                {
                    Id = 2,
                    Name = "Medium"
                },
                new SizeRecord
                {
                    Id = 3,
                    Name = "Large"
                }
            });

            var subject = new SizeRepository(database.Object);
            _result = subject.GetAll();
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void ThenTheSizeRecordIdentifierIsMappedCorrectly(int index, int identifier)
        {
            Assert.That(_result.Sizes[index].Id, Is.EqualTo(identifier));
        }

        [TestCase(0, "Small")]
        [TestCase(1, "Medium")]
        [TestCase(2, "Large")]
        public void ThenTheSizeRecordNameIsMappedCorrectly(int index, string name)
        {
            Assert.That(_result.Sizes[index].Name, Is.EqualTo(name));
        }

        [Test]
        public void ThenAllSizeRecordsAreReturned()
        {
            Assert.That(_result.Sizes.Count, Is.EqualTo(3));
        }
    }
}