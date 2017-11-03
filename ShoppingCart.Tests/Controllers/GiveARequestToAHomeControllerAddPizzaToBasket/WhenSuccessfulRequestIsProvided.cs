using System.Collections.Generic;
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
            _userSessionService.Setup(x => x.AddItemToBasket(It.IsAny<string>(), It.IsAny<BasketData>()));   

            var subject = new HomeController(null, null, null, _userSessionService.Object);
            var context = new Mock<ControllerContext>();
            context.Setup(x => x.HttpContext.Session["UserId"]).Returns<string>(x => "SomeUserIdentifier");
            subject.ControllerContext = context.Object;

            subject.AddPizzaToBasket(1, 1, new List<string>{ "1", "true", "12" });
        }

        [Test]
        public void ThenTheUserSessionServiceIsCalledWithCorrectlyMappedUserToken()
        {
            _userSessionService.Verify(x => x.AddItemToBasket(It.Is<string>(y => y == "SomeUserIdentifier"), It.IsAny<BasketData>()), Times.Once);
        }

        [Test]
        public void ThenTheUserSessionServiceIsCalledWithCorrectlyappedPizzaName()
        {
            _userSessionService.Verify(x => x.AddItemToBasket(It.IsAny<string>(), It.Is<BasketData>(y => y.PizzaId == 1)), Times.Once);
        }

        [Test]
        public void ThenTheUserSessionServiceIsCalledWithCorrectlyappedPizzaSize()
        {
            _userSessionService.Verify(x => x.AddItemToBasket(It.IsAny<string>(), It.Is<BasketData>(y => y.SizeId == 1)), Times.Once);
        }

        [TestCase(1)]
        [TestCase(12)]
        public void ThenTheToppingIdsAreFilteredOutFromTheRequest(int value)
        {
            _userSessionService.Verify(x => x.AddItemToBasket(It.IsAny<string>(), It.Is<BasketData>(y => y.ExtraToppingIds.Contains(value))), Times.Once);
        }
    }
}
