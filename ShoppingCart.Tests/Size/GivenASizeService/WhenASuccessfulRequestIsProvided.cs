using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Size;
using ShoppingCart.Size;

namespace ShoppingCart.Tests.Size.GivenASizeService
{
    [TestFixture]
    public class WhenASuccessfulRequestIsProvided
    {
        private GetAllSizesResponse _result;

        [SetUp]
        public void SetUp()
        {
            var getSizeRepository = new Mock<IGetSizeRepository>();
            getSizeRepository.Setup(x => x.GetAll()).Returns(new GetSizesResponse
            {
                Sizes = new List<SizeRecord>
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
                }
            });

            var subject = new SizeService(getSizeRepository.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Sizes.Count, Is.EqualTo(3));
        }

        [TestCase(0, "Small")]
        [TestCase(1, "Medium")]
        [TestCase(2, "Large")]
        public void ThenThePizzaNameIsMappedThroughCorrectly(int index, string name)
        {
            Assert.That(_result.Sizes[index].Name, Is.EqualTo(name));
        }
    }
}
