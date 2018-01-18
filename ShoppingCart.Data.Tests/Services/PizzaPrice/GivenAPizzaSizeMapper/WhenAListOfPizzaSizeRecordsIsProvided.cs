using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.Data.Services.PizzaPrice;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.Tests.Services.PizzaPrice.GivenAPizzaSizeMapper
{
    [TestFixture]
    public class WhenAListOfPizzaSizeRecordsIsProvided
    {
        private List<PizzaSizeModel> _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var pizzaPrices = new List<PizzaSizeRecord>
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
                    Price = 900
                },
                new PizzaSizeRecord
                {
                    Pizza = new PizzaRecord
                    {
                        Id = 1,
                        Name = "Original"
                    },
                    Size = new SizeRecord
                    {
                        Id = 2,
                        Name = "Medium"
                    },
                    Price = 1100
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
                        Id = 3,
                        Name = "Large"
                    },
                    Price = 1100
                }
            };

            var pizzaToppings = new List<PizzaToppingRecord>
            {
                new PizzaToppingRecord
                {
                    Id = 1,
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
                    Id = 1,
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
            };

            _result = PizzaSizeMapper.Map(pizzaPrices, pizzaToppings);
        }

        [TestCase("Small", 900)]
        [TestCase("Medium", 1100)]
        public void ThenTheSizePricesAreCorrectlyAddedUnderTheSamePizzaName(string sizeName, int priceInPence)
        {
            Assert.That(
                _result.First(x => x.Name == "Original").Sizes
                    .Any(x => x.Key.Name == sizeName && x.Value.InPence == priceInPence), Is.True);
        }

        [TestCase("Cheese")]
        [TestCase("Bacon")]
        public void ThenThePizzaToppingsAreCorrectlyAddedUnderTheSamePizzaName(string name)
        {
            Assert.That(_result.First(x => x.Name == "Original").Toppings.Any(x => x.Name == name), Is.True);
        }

        [Test]
        public void ThenOnlyOnePizzaIsAdded()
        {
            Assert.That(_result.Count(x => x.Name == "Original"), Is.EqualTo(1));
        }
    }
}