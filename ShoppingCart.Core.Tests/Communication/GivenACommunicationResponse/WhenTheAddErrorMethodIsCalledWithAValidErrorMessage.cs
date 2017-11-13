using NUnit.Framework;
using ShoppingCart.Core.Communication;

namespace ShoppingCart.Core.Tests.Communication.GivenACommunicationResponse
{
    [TestFixture]
    public class WhenTheAddErrorMethodIsCalledWithAValidErrorMessage
    {
        private CommunicationResponse _subject;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = new CommunicationResponse();

            _subject.AddError(new Error
            {
                TechnicalMessage = "Some error message has occured."
            });
        }

        [Test]
        public void ThenTheHasErrorPropertyIsSetToTrue()
        {
            Assert.That(_subject.HasError, Is.True);
        }

        [Test]
        public void ThenTheErrorMessageIsMappedCorreclty()
        {
            Assert.That(_subject.Error.TechnicalMessage, Is.EqualTo("Some error message has occured."));
        }
    }
}
