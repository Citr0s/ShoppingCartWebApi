using NUnit.Framework;

namespace ShoppingCart.Core.Tests.Money.GivenARequestToConvertPenceToMoney
{
    [TestFixture]
    public class WhenAValueGreatThanZeroIsProvided
    {
        private Core.Money.Money _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _result = Core.Money.Money.From(1650);
        }

        [Test]
        public void ThenThePriceInPenceIsCorrectlyAssigned()
        {
            Assert.That(_result.InPence, Is.EqualTo(1650));
        }

        [Test]
        public void ThenThePriceInPoundsIsCorrectlyCalculated()
        {
            Assert.That(_result.InPounds, Is.EqualTo(16.50));
        }

        [Test]
        public void ThenThePriceInFullIsCorrectlyGenerated()
        {
            Assert.That(_result.InFull, Is.EqualTo("£16.50"));
        }
    }
}