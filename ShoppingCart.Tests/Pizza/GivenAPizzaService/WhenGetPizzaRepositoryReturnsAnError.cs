using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaPrice;
using ShoppingCart.Pizza;

namespace ShoppingCart.Tests.Pizza.GivenAPizzaService
{
    [TestFixture]
    public class WhenGetPizzaRepositoryReturnsAnError
    {
        private GetAllPizzasResponse _result;

        [SetUp]
        public void SetUp()
        {
            var getPizzaRepository = new Mock<IGetPizzaPriceRepository>();
            getPizzaRepository.Setup(x => x.GetAll()).Returns(new GetPizzaPricesResponse
            {
                HasError = true,
                Error = new Error
                {
                    Message = "Something went wrong when retrieving PizzaRecords."
                }
            });

            var subject = new PizzaService(getPizzaRepository.Object);
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
            Assert.That(_result.Pizzas.Count, Is.Zero);
        }
    }
}
