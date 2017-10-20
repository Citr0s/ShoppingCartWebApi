using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ShoppingCart.HomePage;
using ShoppingCart.UserSession;

namespace ShoppingCart.Tests.Controllers.GiveARequestToAHomeControllerAddPizzaToBasket
{
    [TestFixture]
    public class WhenSuccessfulRequestIsProvided
    {
        private Mock<IUserSessionService> _userSessionService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userSessionService = new Mock<IUserSessionService>();
            _userSessionService.Setup(x => x.AddItemToBasket(It.IsAny<string>(), It.IsAny<BasketItem>()));   

            var subject = new HomeController(null, _userSessionService.Object);
            var context = new Mock<ControllerContext>();
            context.Setup(x => x.HttpContext.Session["UserId"]).Returns<string>(x => "SomeUserIdentifier");
            subject.ControllerContext = context.Object;

            subject.AddPizzaToBasket("Original", "Small", 900);
        }

        [Test]
        public void ThenTheUserSessionServiceIsCalledWithCorrectlyMappedUserToken()
        {
            _userSessionService.Verify(x => x.AddItemToBasket(It.Is<string>(y => y == "SomeUserIdentifier"), It.IsAny<BasketItem>()), Times.Once);
        }

        [Test]
        public void ThenTheUserSessionServiceIsCalledWithCorrectlyappedPizzaName()
        {
            _userSessionService.Verify(x => x.AddItemToBasket(It.IsAny<string>(), It.Is<BasketItem>(y => y.Name == "Original")), Times.Once);
        }

        [Test]
        public void ThenTheUserSessionServiceIsCalledWithCorrectlyappedPizzaSize()
        {
            _userSessionService.Verify(x => x.AddItemToBasket(It.IsAny<string>(), It.Is<BasketItem>(y => y.Size == "Small")), Times.Once);
        }

        [Test]
        public void ThenTheUserSessionServiceIsCalledWithCorrectlyappedPizzaPrice()
        {
            _userSessionService.Verify(x => x.AddItemToBasket(It.IsAny<string>(), It.Is<BasketItem>(y => y.Price.InPence == 900)), Times.Once);
        }
    }
}
