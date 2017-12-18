using NUnit.Framework;
using ShoppingCart.Core.Email;

namespace ShoppingCart.Core.Tests.Email.GivenARequestToCheckIfEmailIsValid
{
    [TestFixture]
    public class WhenCorrectEmailIsProvided
    {
        private bool _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _result = EmailValidator.IsValid("some@email.com");
        }

        [Test]
        public void ThenValidationPasses()
        {
            Assert.That(_result, Is.True);
        }
    }
}