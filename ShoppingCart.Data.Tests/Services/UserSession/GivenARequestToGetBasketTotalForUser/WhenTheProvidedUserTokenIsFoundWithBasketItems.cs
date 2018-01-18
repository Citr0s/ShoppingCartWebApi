﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Money;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Services.UserSession;
using ShoppingCart.Data.Services.Voucher;
using ShoppingCart.Data.ToppingSize;

namespace ShoppingCart.Data.Tests.Services.UserSession.GivenARequestToGetBasketTotalForUser
{
    [TestFixture]
    public class WhenTheProvidedUserTokenIsFoundWithBasketItems
    {
        private Money _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var pizzaSizeRepository = new Mock<IPizzaSizeRepository>();
            pizzaSizeRepository.Setup(x => x.GetByIds(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
                new GetPizzaSizeResponse {PizzaSize = new PizzaSizeRecord {Price = 1500}});

            var toppingSizeRepository = new Mock<IToppingSizeRepository>();
            toppingSizeRepository.Setup(x => x.GetByIds(It.IsAny<List<int>>(), It.IsAny<int>()))
                .Returns(() => new GetToppingSizeResponse());

            var voucherService = new Mock<IVoucherService>();

            var subject = new UserSessionService(pizzaSizeRepository.Object, toppingSizeRepository.Object, voucherService.Object);
            var userToken = subject.NewUser();
            subject.AddItemToBasket(userToken, new BasketData {PizzaId = 1, SizeId = 1});

            _result = subject.GetBasketTotalForUser(userToken);
        }

        [Test]
        public void ThenCorrectTotalIsReturned()
        {
            Assert.That(_result.InPence, Is.EqualTo(1500));
        }
    }
}