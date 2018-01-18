using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.Data.Services.PizzaPrice;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.Tests.Services.PizzaPrice.GivenAPizzaPriceService
{
    [TestFixture]
    public class WhenASuccessfulRequestIsProvided
    {
        private GetAllPizzaSizesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var getPizzaPriceRepository = new Mock<IPizzaSizeRepository>();
            getPizzaPriceRepository.Setup(x => x.GetAll()).Returns(new GetPizzaSizesResponse
            {
                PizzaSizes = new List<PizzaSizeRecord>
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

            var pizzaToppingRepository = new Mock<IPizzaToppingRepository>();
            pizzaToppingRepository.Setup(x => x.GetAll()).Returns(new GetPizzaToppingResponse
            {
                PizzaToppings = new List<PizzaToppingRecord>
                {
                    new PizzaToppingRecord
                    {
                        Pizza = new PizzaRecord
                        {
                            Id = 1,
                            Name = "Original"
                        },
                        Topping = new ToppingRecord
                        {
                            Id = 1,
                            Name = "Cheese"
                        }
                    },
                    new PizzaToppingRecord
                    {
                        Pizza = new PizzaRecord
                        {
                            Id = 1,
                            Name = "Original"
                        },
                        Topping = new ToppingRecord
                        {
                            Id = 2,
                            Name = "Bacon"
                        }
                    }
                }
            });

            var subject = new PizzaSizeService(getPizzaPriceRepository.Object, pizzaToppingRepository.Object);
            _result = subject.GetAll();
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

        [TestCase("Cheese")]
        [TestCase("Bacon")]
        public void ThenThePizzaToppingIsMappedThroughCorrectly(string name)
        {
            Assert.That(_result.Pizzas[0].Toppings.Any(x => x.Name == name), Is.True);
        }

        [Test]
        public void ThenAListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Pizzas.Count, Is.EqualTo(2));
        }
    }
}