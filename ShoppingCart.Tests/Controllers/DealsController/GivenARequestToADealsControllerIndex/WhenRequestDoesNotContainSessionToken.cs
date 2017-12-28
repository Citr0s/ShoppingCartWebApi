using System;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Money;
using ShoppingCart.Services.UserSession;
using ShoppingCart.Services.Voucher;

namespace ShoppingCart.Tests.Controllers.DealsController.GivenARequestToADealsControllerIndex
{
    [TestFixture]
    public class WhenRequestDoesNotContainSessionToken
    {
        private Mock<IUserSessionService> _userSessionService;
        private Mock<IVoucherService> _voucherService;

        [OneTimeSetUp]
        public void SetUp()
        {
            var userToken = Guid.NewGuid().ToString();

            _userSessionService = new Mock<IUserSessionService>();
            _userSessionService.Setup(x => x.NewUser()).Returns(() => userToken);
            _userSessionService.Setup(x => x.GetVoucherForUser(It.IsAny<string>())).Returns(() => new VoucherDetailsModel());
            _userSessionService.Setup(x => x.GetBasketTotalForUser(It.IsAny<string>())).Returns(() => new Money());
            _userSessionService.Setup(x => x.IsLoggedIn(It.IsAny<string>())).Returns(() => false);
            
            _voucherService = new Mock<IVoucherService>();
            _voucherService.Setup(x => x.GetAll()).Returns(() => new GetAllVouchersResponse());

            var subject = new ShoppingCart.Controllers.Deals.DealsController(_userSessionService.Object, _voucherService.Object);
            var context = new Mock<ControllerContext>();

            var firstRun = true;
            context.Setup(x => x.HttpContext.Session["UserId"])
                .Returns(() =>
                {
                    if (!firstRun)
                        return userToken;

                    firstRun = false;
                    return null;
                });

            subject.ControllerContext = context.Object;

            subject.Index();
        }

        [Test]
        public void ThenNewUserTokenIsGenerated()
        {
            _userSessionService.Verify(x => x.NewUser(), Times.Once);
        }
    }
}