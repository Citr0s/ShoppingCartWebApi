using System;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using GetAllVouchersResponse = ShoppingCart.Data.Services.Voucher.GetAllVouchersResponse;
using GetVoucherByIdResponse = ShoppingCart.Data.Services.Voucher.GetVoucherByIdResponse;

namespace ShoppingCart.Tests.Controllers.DealsController.GivenARequestToADealsControllerApplyDeal
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
            _voucherService.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => new GetVoucherByIdResponse
            {
                HasError = true
            });

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

            subject.ApplyDeal(1);
        }

        [Test]
        public void ThenDealIsNeverAssignedToAUser()
        {
            _userSessionService.Verify(x => x.SelectDeal(It.IsAny<string>(), It.IsAny<VoucherDetailsModel>()), Times.Never);
        }
    }
}