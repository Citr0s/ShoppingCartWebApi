using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Size;
using ShoppingCart.Size;

namespace ShoppingCart.Tests.Size.GivenASizeService
{
    [TestFixture]
    public class WhenGetSizeRepositoryReturnsAnError
    {
        private GetAllSizesResponse _result;

        [SetUp]
        public void SetUp()
        {
            var getSizeRespository = new Mock<ISizeRepository>();
            getSizeRespository.Setup(x => x.GetAll()).Returns(new GetSizesResponse
            {
                HasError = true,
                Error = new Error
                {
                    Message = "Something went wrong when retrieving SizeRecords."
                }
            });

            var subject = new SizeService(getSizeRespository.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenAnErrorMessageIsReturned()
        {
            Assert.That(_result.Error.Message, Is.EqualTo("Something went wrong when retrieving SizeRecords."));
        }

        [Test]
        public void ThenAnEmptyListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Sizes.Count, Is.Zero);
        }
    }
}
