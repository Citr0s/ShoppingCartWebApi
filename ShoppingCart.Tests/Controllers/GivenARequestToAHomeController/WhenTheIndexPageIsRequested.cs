using Moq;
using NUnit.Framework;
using ShoppingCart.Controllers;
using ShoppingCart.Pizza;

namespace ShoppingCart.Tests.Controllers.GivenARequestToAHomeController
{
    [TestFixture]
    public class WhenTheIndexPageIsRequested
    {
        private Mock<IPizzaService> _pizzaService;

        [SetUp]
        public void SetUp()
        {
            _pizzaService = new Mock<IPizzaService>();
            _pizzaService.Setup(x => x.GetAll()).Returns(new GetAllPizzasResponse());

            var subject = new HomeController(_pizzaService.Object);

            subject.Index();
        }

        [Test]
        public void ThenThePizzaServiceIsCalled()
        {
            _pizzaService.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
