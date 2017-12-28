using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Topping;
using ShoppingCart.Services.Topping;

namespace ShoppingCart.Tests.Services.Topping.GivenAToppingService
{
    [TestFixture]
    public class WhenGetToppingRepositoryReturnsAnError
    {
        private GetAllToppingsResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var getToppingRepository = new Mock<IToppingRepository>();
            getToppingRepository.Setup(x => x.GetAll()).Returns(new GetToppingsResponse
            {
                HasError = true,
                Error = new Error
                {
                    UserMessage = "Something went wrong when retrieving ToppingRecords."
                }
            });

            var subject = new ToppingService(getToppingRepository.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAnEmptyListOfToppingModelsIsReturned()
        {
            Assert.That(_result.Toppings.Count, Is.Zero);
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenAnErrorMessageIsReturned()
        {
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Something went wrong when retrieving ToppingRecords."));
        }
    }
}