using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Services.Size;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.Tests.Services.Size.GivenASizeService
{
    [TestFixture]
    public class WhenASuccessfulRequestIsProvided
    {
        private GetAllSizesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var getSizeRepository = new Mock<ISizeRepository>();
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

        [TestCase(0, "Small")]
        [TestCase(1, "Medium")]
        [TestCase(2, "Large")]
        public void ThenThePizzaNameIsMappedThroughCorrectly(int index, string name)
        {
            Assert.That(_result.Sizes[index].Name, Is.EqualTo(name));
        }

        [Test]
        public void ThenAListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Sizes.Count, Is.EqualTo(3));
        }
    }
}