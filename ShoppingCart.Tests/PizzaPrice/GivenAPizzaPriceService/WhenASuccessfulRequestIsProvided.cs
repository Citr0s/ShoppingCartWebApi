using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Size;
using ShoppingCart.PizzaPrice;

namespace ShoppingCart.Tests.PizzaPrice.GivenAPizzaPriceService
{
    [TestFixture]
    public class WhenASuccessfulRequestIsProvided
    {
        private GetAllPizzaSizesResponse _result;

        [SetUp]
        public void SetUp()
        {
            var getPizzaPriceRepository = new Mock<IPizzaSizeRepository>();
            getPizzaPriceRepository.Setup(x => x.GetAll()).Returns(new GetPizzaSizesResponse
            {
                PizzaPrices = new List<PizzaSizeRecord>
                {
                    new PizzaSizeRecord
                    {
                        Pizza = new PizzaRecord
                        {
                            Id = 1,
                            Name = "Original"
                        },
                        Size = new SizeRecord
                        {
                            Id = 1,
                            Name = "Small"
                        },
                        Price = 800
                    },
                    new PizzaSizeRecord
                    {
                        Pizza = new PizzaRecord
                        {
                            Id = 2,
                            Name = "Veggie Delight"
                        },
                        Size = new SizeRecord
                        {
                            Id = 1,
                            Name = "Medium"
                        },
                        Price = 1100
                    }
                }
            });

            var subject = new PizzaSizeService(getPizzaPriceRepository.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Pizzas.Count, Is.EqualTo(2));
        }

        [TestCase(0, "Original")]
        [TestCase(1, "Veggie Delight")]
        public void ThenThePizzaNameIsMappedThroughCorrectly(int index, string name)
        {
            Assert.That(_result.Pizzas[index].Name, Is.EqualTo(name));
        }

        [TestCase(0, "Small")]
        [TestCase(1, "Medium")]
        public void ThenThePizzaSizeIsMappedThroughCorrectly(int index, string name)
        {
            Assert.That(_result.Pizzas[index].Sizes.Any(x => x.Key.Name == name), Is.True);
        }

        [TestCase(0, 800)]
        [TestCase(1, 1100)]
        public void ThenThePizzaPriceIsMappedThroughCorrectly(int index, int price)
        {
            Assert.That(_result.Pizzas[index].Sizes.Any(x => x.Value.InPence == price), Is.True);
        }
    }
}
