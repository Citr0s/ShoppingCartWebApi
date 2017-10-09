using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Controllers;
using ShoppingCart.Pizza;
using ShoppingCart.Size;
using ShoppingCart.Topping;

namespace ShoppingCart.Tests.Controllers.GivenARequestToAHomeController
{
    [TestFixture]
    public class WhenTheIndexPageIsRequested
    {
        private Mock<IPizzaService> _pizzaService;
        private Mock<ISizeService> _sizeService;
        private Mock<IToppingService> _toppingService;

        [SetUp]
        public void SetUp()
        {
            _pizzaService = new Mock<IPizzaService>();
            _pizzaService.Setup(x => x.GetAll()).Returns(new GetAllPizzasResponse
            {
                Pizzas = new List<PizzaPriceModel>
                {
                    new PizzaPriceModel
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

            _toppingService = new Mock<IToppingService>();
            _toppingService.Setup(x => x.GetAll()).Returns(new GetAllToppingsResponse
            {
                Toppings = new List<ToppingModel>
                {
                    new ToppingModel
                    {
                        Name = "Onion"
                    }
                }
            });

            var subject = new HomeController(_pizzaService.Object, _sizeService.Object, _toppingService.Object);

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

        [Test]
        public void ThenTheToppingServiceIsCalled()
        {
            _toppingService.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
