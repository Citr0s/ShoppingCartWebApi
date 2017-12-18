using NUnit.Framework;
using ShoppingCart.Data.IoC;

namespace ShoppingCart.Data.Tests.IoC.GivenARequestToRegisterAnAdapter
{
    [TestFixture]
    public class WhenTheAdapterIsRegistered
    {
        private IExample _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new Data.IoC.IoC();
            subject.Register<IExample>(new Example());

            _result = subject.For<IExample>();
        }

        [Test]
        public void ThenTheConcreteObjectCanBeReturned()
        {
            Assert.That(_result.Id, Is.EqualTo(42));
        }
    }

    public interface IExample : IAdapter
    {
        int Id { get; set; }
    }

    public class Example : IExample
    {
        public Example()
        {
            Id = 42;
        }

        public int Id { get; set; }
    }
}