﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Services.Topping;
using ShoppingCart.Data.Topping;

namespace ShoppingCart.Data.Tests.Services.Topping.GivenAToppingService
{
    [TestFixture]
    public class WhenASuccessfulRequestIsProvided
    {
        private GetAllToppingsResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var getToppingRepository = new Mock<IToppingRepository>();
            getToppingRepository.Setup(x => x.GetAll()).Returns(new GetToppingsResponse
            {
                Toppings = new List<ToppingRecord>
                {
                    new ToppingRecord
                    {
                        Id = 1,
                        Name = "Onion"
                    },
                    new ToppingRecord
                    {
                        Id = 2,
                        Name = "Ham"
                    }
                }
            });

            var subject = new ToppingService(getToppingRepository.Object);
            _result = subject.GetAll();
        }

        [TestCase(0, "Onion")]
        [TestCase(1, "Ham")]
        public void ThenThePizzaNameIsMappedThroughCorrectly(int index, string name)
        {
            Assert.That(_result.Toppings[index].Name, Is.EqualTo(name));
        }

        [Test]
        public void ThenAListOfPizzaModelsIsReturned()
        {
            Assert.That(_result.Toppings.Count, Is.EqualTo(2));
        }
    }
}