using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.PizzaSize;
using ShoppingCart.Data.PizzaTopping;
using ShoppingCart.Data.Size;
using ShoppingCart.Services.PizzaPrice;

namespace ShoppingCart.Tests.PizzaPrice.GivenAPizzaPriceService
{
    [TestFixture]
    public class WhenGetPizzaToppingsRepositoryReturnsAnError
    {
        private GetAllPizzaSizesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var getPizzaPriceRepository = new Mock<IPizzaSizeRepository>();
            getPizzaPriceRepository.Setup(x => x.GetAll()).Returns(new GetPizzaSizesResponse
            {
                PizzaSizes = new List<PizzaSizeRecord>
                {
                    new PizzaSizeRecord
                    {
                        Pizza = new PizzaRecord
                        {
                            Id = 1,
                            Name = "Original"
                        },
                        Size = new SizeRecord
                        {
                            Id = 1,
                            Name = "Small"
                        },
                        Price = 800
                    },
                    new PizzaSizeRecord
                    {
                        Pizza = new PizzaRecord
                        {
                            Id = 2,
                            Name = "Veggie Delight"
                        },
                        Size = new SizeRecord
                        {
                            Id = 1,
                            Name = "Medium"
                        },
                        Price = 1100
                    }
                }
            });

            var pizzaToppingRepository = new Mock<IPizzaToppingRepository>();
            pizzaToppingRepository.Setup(x => x.GetAll()).Returns(new GetPizzaToppingResponse
            {
                HasError = true,
                Error = new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Something went wrong"
                }
            });

            var subject = new PizzaSizeService(getPizzaPriceRepository.Object, pizzaToppingRepository.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenTheCorrectErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }

        [Test]
        public void ThenTheErrorMessageIsMappedToTheResponse()
        {
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Something went wrong"));
        }
    }
}