using Moq;
using NUnit.Framework;
using ShoppingCart.Controllers;
using ShoppingCart.Pizza;
using ShoppingCart.PizzaPrice;

namespace ShoppingCart.Tests.Controllers.GivenARequestToAHomeController
{
    [TestFixture]
    public class WhenTheIndexPageIsRequested
    {
        private Mock<IPizzaSizeService> _pizzaService;

        [SetUp]
        public void SetUp()
        {
            _pizzaService = new Mock<IPizzaSizeService>();
            _pizzaService.Setup(x => x.GetAll()).Returns(new GetAllPizzaSizesResponse());

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
