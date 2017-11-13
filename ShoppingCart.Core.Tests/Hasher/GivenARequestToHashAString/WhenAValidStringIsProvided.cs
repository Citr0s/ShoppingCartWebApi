using NUnit.Framework;

namespace ShoppingCart.Core.Tests.Hasher.GivenARequestToHashAString
{
    [TestFixture]
    public class WhenAValidStringIsProvided
    {
        private string _result;
        private Core.Hasher.Hasher _subject;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = new Core.Hasher.Hasher();
            _result = _subject.Hash("SOME_STRING");
        }

        [Test]
        public void ThenTheStringIsHashedCorrectly()
        {
            Assert.That(_result, Is.Not.EqualTo("SOME_STRING"));
        }

        [Test]
        public void ThenTheSameResultCanBeAchievedEveryTime()
        {
            Assert.That(_result, Is.EqualTo(_subject.Hash("SOME_STRING")));
        }
    }
}
