using NUnit.Framework;
using ShoppingCart.Core.Email;

namespace ShoppingCart.Core.Tests.Email.GivenARequestToCheckIfEmailIsValid
{
    [TestFixture]
    public class WhenInvalidEmailIsProvided
    {
        private bool _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _result = EmailValidator.IsValid("some_invalid_email");
        }

        [Test]
        public void ThenValidationPasses()
        {
            Assert.That(_result, Is.False);
        }
    }
}