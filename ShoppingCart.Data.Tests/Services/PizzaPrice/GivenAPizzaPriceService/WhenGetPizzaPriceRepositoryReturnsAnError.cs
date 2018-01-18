﻿using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.Services.PizzaPrice;

namespace ShoppingCart.Data.Tests.Services.PizzaPrice.GivenAPizzaPriceService
{
    [TestFixture]
    public class WhenGetPizzaPriceRepositoryReturnsAnError
    {
        private GetAllPizzaSizesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var getPizzaPriceRepository = new Mock<IPizzaSizeRepository>();
            getPizzaPriceRepository.Setup(x => x.GetAll()).Returns(new GetPizzaSizesResponse
            {
                HasError = true,
                Error = new Error
                {
                    UserMessage = "Something went wrong when retrieving PizzaRecords."
                }
            });

            var subject = new PizzaSizeService(getPizzaPriceRepository.Object, null);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAnEmptyListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Pizzas.Count, Is.Zero);
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenAnErrorMessageIsReturned()
        {
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Something went wrong when retrieving PizzaRecords."));
        }
    }
}