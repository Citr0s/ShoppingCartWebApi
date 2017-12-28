using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Money;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;

namespace ShoppingCart.Tests.Controllers.DealsController.GivenARequestToADealsControllerIndex
{
    [TestFixture]
    public class WhenRequestContainsSessionToken
    {
        private Mock<IVoucherService> _voucherService;
        private Mock<IUserSessionService> _userSessionService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userSessionService = new Mock<IUserSessionService>();
            _userSessionService.Setup(x => x.GetVoucherForUser(It.IsAny<string>())).Returns(() => new VoucherDetailsModel());
            _userSessionService.Setup(x => x.GetBasketTotalForUser(It.IsAny<string>())).Returns(() => new Money());
            _userSessionService.Setup(x => x.IsLoggedIn(It.IsAny<string>())).Returns(() => false);

            _voucherService = new Mock<IVoucherService>();
            _voucherService.Setup(x => x.GetAll()).Returns(() => new GetAllVouchersResponse());

            var subject = new ShoppingCart.Controllers.Deals.DealsController(_userSessionService.Object, _voucherService.Object);
            var context = new Mock<ControllerContext>();
            context.Setup(x => x.HttpContext.Session["UserId"]).Returns<string>(x => "SomeUserIdentifier");
            subject.ControllerContext = context.Object;

            subject.Index();
        }

        [Test]
        public void ThenTheVouchersForUsersAreCalledWithCorrectlyMappedUserToken()
        {
            _userSessionService.Verify(x => x.GetVoucherForUser(It.Is<string>(y => y == "SomeUserIdentifier")),
                Times.Once);
        }

        [Test]
        public void ThenTheBasketForUsersAreCalledWithCorrectlyMappedUserToken()
        {
            _userSessionService.Verify(x => x.GetBasketTotalForUser(It.Is<string>(y => y == "SomeUserIdentifier")),
                Times.Once);
        }

        [Test]
        public void ThenTheLoggedInUsersAreCalledWithCorrectlyMappedUserToken()
        {
            _userSessionService.Verify(x => x.IsLoggedIn(It.Is<string>(y => y == "SomeUserIdentifier")),
                Times.Once);
        }

        [Test]
        public void ThenTheVouchersGetAllMethodIsCalled()
        {
            _voucherService.Verify(x => x.GetAll(),
                Times.Once);
        }
    }
}