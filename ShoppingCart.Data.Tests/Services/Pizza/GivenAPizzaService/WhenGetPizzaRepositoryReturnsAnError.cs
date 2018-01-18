using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Services.Pizza;

namespace ShoppingCart.Data.Tests.Services.Pizza.GivenAPizzaService
{
    [TestFixture]
    public class WhenGetPizzaRepositoryReturnsAnError
    {
        private GetAllPizzasResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var getPizzaRepository = new Mock<IPizzaRepository>();
            getPizzaRepository.Setup(x => x.GetAll()).Returns(new GetPizzasResponse
            {
                HasError = true,
                Error = new Error
                {
                    UserMessage = "Something went wrong when retrieving PizzaRecords."
                }
            });

            var subject = new PizzaService(getPizzaRepository.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAnEmptyListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Pizzas.Count, Is.Zero);
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenAnErrorMessageIsReturned()
        {
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Something went wrong when retrieving PizzaRecords."));
        }
    }
}