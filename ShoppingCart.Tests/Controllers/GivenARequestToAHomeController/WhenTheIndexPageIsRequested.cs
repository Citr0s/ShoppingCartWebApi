using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Controllers;
using ShoppingCart.Pizza;
using ShoppingCart.Size;

namespace ShoppingCart.Tests.Controllers.GivenARequestToAHomeController
{
    [TestFixture]
    public class WhenTheIndexPageIsRequested
    {
        private Mock<IPizzaService> _pizzaService;
        private Mock<ISizeService> _sizeService;

        [SetUp]
        public void SetUp()
        {
            _pizzaService = new Mock<IPizzaService>();
            _pizzaService.Setup(x => x.GetAll()).Returns(new GetAllPizzasResponse
            {
                Pizzas = new List<PizzaModel>
                {
                    new PizzaModel
                    {
                        Name = "Original"
                    }
                }
            });

            _sizeService = new Mock<ISizeService>();
            _sizeService.Setup(x => x.GetAll()).Returns(new GetAllSizesResponse
            {
                Sizes = new List<SizeModel>
                {
                    new SizeModel
                    {
                        Name = "Small"
                    }
                }
            });

            var subject = new HomeController(_pizzaService.Object, _sizeService.Object);

            subject.Index();
        }

        [Test]
        public void ThenThePizzaServiceIsCalled()
        {
            _pizzaService.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void ThenTheSizeServiceIsCalled()
        {
            _sizeService.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
