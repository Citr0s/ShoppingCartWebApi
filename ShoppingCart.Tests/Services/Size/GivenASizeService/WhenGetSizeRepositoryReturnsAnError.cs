using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Size;
using ShoppingCart.Services.Size;

namespace ShoppingCart.Tests.Services.Size.GivenASizeService
{
    [TestFixture]
    public class WhenGetSizeRepositoryReturnsAnError
    {
        private GetAllSizesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var getSizeRespository = new Mock<ISizeRepository>();
            getSizeRespository.Setup(x => x.GetAll()).Returns(new GetSizesResponse
            {
                HasError = true,
                Error = new Error
                {
                    UserMessage = "Something went wrong when retrieving SizeRecords."
                }
            });

            var subject = new SizeService(getSizeRespository.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAnEmptyListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Sizes.Count, Is.Zero);
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenAnErrorMessageIsReturned()
        {
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Something went wrong when retrieving SizeRecords."));
        }
    }
}