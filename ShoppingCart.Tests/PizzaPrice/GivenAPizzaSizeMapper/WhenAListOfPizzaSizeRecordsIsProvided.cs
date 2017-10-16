using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Size;
using ShoppingCart.PizzaPrice;

namespace ShoppingCart.Tests.PizzaPrice.GivenAPizzaSizeMapper
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
                        Name = "Original"
                    },
                    Size = new SizeRecord
                    {
                        Name = "Small"
                    },
                    Price = 900
                },
                new PizzaSizeRecord
                {
                    Pizza = new PizzaRecord
                    {
                        Name = "Original"
                    },
                    Size = new SizeRecord
                    {
                        Name = "Medium"
                    },
                    Price = 1100
                },
                new PizzaSizeRecord
                {
                    Pizza = new PizzaRecord
                    {
                        Name = "Veggie Delight"
                    },
                    Size = new SizeRecord
                    {
                        Name = "Large"
                    },
                    Price = 1100
                }
            };

            _result = PizzaSizeMapper.Map(pizzaPrices);
        }

        [TestCase("Small", 900)]
        [TestCase("Medium", 1100)]
        public void ThenTheSizePricesAreCorrectlyAddedUnderTheSamePizzaName(string sizeName, int priceInPence)
        {
            Assert.That(_result.First(x => x.Name == "Original").Sizes.Any(x => x.Key.Name == sizeName && x.Value.InPence == priceInPence), Is.True);
        }

        [Test]
        public void ThenOnlyOnePizzaIsAdded()
        {
            Assert.That(_result.Count(x => x.Name == "Original"), Is.EqualTo(1));
        }
    }
}
