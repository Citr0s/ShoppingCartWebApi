using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Services.Pizza;

namespace ShoppingCart.Data.Tests.Services.Pizza.GivenAPizzaService
{
    [TestFixture]
    public class WhenASuccessfulRequestIsProvided
    {
        private GetAllPizzasResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var getPizzaRepository = new Mock<IPizzaRepository>();
            getPizzaRepository.Setup(x => x.GetAll()).Returns(new GetPizzasResponse
            {
                Pizzas = new List<PizzaRecord>
                {
                    new PizzaRecord
                    {
                        Id = 1,
                        Name = "Original"
                    },
                    new PizzaRecord
                    {
                        Id = 2,
                        Name = "Veggie Delight"
                    }
                }
            });

            var subject = new PizzaService(getPizzaRepository.Object);
            _result = subject.GetAll();
        }

        [TestCase(0, "Original")]
        [TestCase(1, "Veggie Delight")]
        public void ThenThePizzaNameIsMappedThroughCorrectly(int index, string name)
        {
            Assert.That(_result.Pizzas[index].Name, Is.EqualTo(name));
        }

        [Test]
        public void ThenAListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Pizzas.Count, Is.EqualTo(2));
        }
    }
}