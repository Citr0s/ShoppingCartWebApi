using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.Voucher;
using GetVoucherByIdResponse = ShoppingCart.Data.Services.Voucher.GetVoucherByIdResponse;

namespace ShoppingCart.Tests.Controllers.DealsController.GivenARequestToADealsControllerApplyDeal
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
            _userSessionService.Setup(x => x.SelectDeal(It.IsAny<string>(), It.IsAny<VoucherDetailsModel>()));

            _voucherService = new Mock<IVoucherService>();
            _voucherService.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => new GetVoucherByIdResponse
            {
                Voucher = new VoucherRecord
                {
                    Id = 1,
                    Code = "SOME_CODE",
                    OnlyNamed = true,
                    Price = "1200",
                    Quantity = 2,
                    Title = "Awesome Deal"
                }
            });

            var subject = new ShoppingCart.Controllers.Deals.DealsController(_userSessionService.Object, _voucherService.Object);
            var context = new Mock<ControllerContext>();
            context.Setup(x => x.HttpContext.Session["UserId"]).Returns<string>(x => "SomeUserIdentifier");
            subject.ControllerContext = context.Object;

            subject.ApplyDeal(1);
        }

        [Test]
        public void ThenTheSelectDealForUsersAreCalledWithCorrectlyMappedUserToken()
        {
            _userSessionService.Verify(x => x.SelectDeal(It.Is<string>(y => y == "SomeUserIdentifier"), It.IsAny<VoucherDetailsModel>()),
                Times.Once);
        }
    }
}