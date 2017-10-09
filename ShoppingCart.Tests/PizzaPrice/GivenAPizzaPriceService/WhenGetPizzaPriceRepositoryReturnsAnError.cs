using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.PizzaPrice;
using ShoppingCart.PizzaPrice;

namespace ShoppingCart.Tests.PizzaPrice.GivenAPizzaPriceService
{
    [TestFixture]
    public class WhenGetPizzaPriceRepositoryReturnsAnError
    {
        private GetAllPizzaPricesResponse _result;

        [SetUp]
        public void SetUp()
        {
            var getPizzaPriceRepository = new Mock<IPizzaPriceRepository>();
            getPizzaPriceRepository.Setup(x => x.GetAll()).Returns(new GetPizzaPricesResponse
            {
                HasError = true,
                Error = new Error
                {
                    Message = "Something went wrong when retrieving PizzaRecords."
                }
            });

            var subject = new PizzaPriceService(getPizzaPriceRepository.Object);
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
            Assert.That(_result.Error.Message, Is.EqualTo("Something went wrong when retrieving PizzaRecords."));
        }

        [Test]
        public void ThenAnEmptyListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.PizzaPrices.Count, Is.Zero);
        }
    }
}
