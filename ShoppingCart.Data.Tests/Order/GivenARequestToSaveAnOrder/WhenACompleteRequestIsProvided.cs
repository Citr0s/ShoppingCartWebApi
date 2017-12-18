using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Controllers.Basket;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Order;
using ShoppingCart.Data.Pizza;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Topping;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Tests.Order.GivenARequestToSaveAnOrder
{
    [TestFixture]
    public class WhenACompleteRequestIsProvided
    {
        private SaveOrderResponse _result;
        private Mock<IDatabase> _database;

        [OneTimeSetUp]
        public void SetUp()
        {
            _database = new Mock<IDatabase>();
            _database.Setup(x => x.Query<UserRecord>()).Returns(new List<UserRecord> {new UserRecord {Id = 1}});
            _database.Setup(x => x.Query<BasketRecord>()).Returns(new List<BasketRecord> {new BasketRecord {Id = 1}});
            _database.Setup(x => x.Query<PizzaRecord>()).Returns(new List<PizzaRecord> {new PizzaRecord {Id = 2}});
            _database.Setup(x => x.Query<SizeRecord>()).Returns(new List<SizeRecord> {new SizeRecord {Id = 3}});
            _database.Setup(x => x.Query<OrderRecord>()).Returns(new List<OrderRecord> {new OrderRecord {Id = 2}});
            _database.Setup(x => x.Query<ToppingRecord>())
                .Returns(new List<ToppingRecord> {new ToppingRecord {Id = 4}});

            _database.Setup(x => x.Save(It.IsAny<BasketRecord>())).Returns(new BasketRecord {Id = 1});
            _database.Setup(x => x.Save(It.IsAny<OrderRecord>())).Returns(new OrderRecord {Id = 2});
            _database.Setup(x => x.Save(It.IsAny<OrderToppingRecord>())).Returns(new OrderToppingRecord {Id = 3});

            var subject = new OrderRepository(_database.Object);
            var saveOrderRequest = new SaveOrderRequest
            {
                DeliveryType = DeliveryType.Collection.ToString(),
                GrandTotal = 1200,
                Voucher = "SOME_VOUCHER",
                UserId = 1,
                Status = OrderStatus.Complete.ToString(),
                Orders = new List<Data.Order.Order>
                {
                    new Data.Order.Order
                    {
                        SubTotal = 1000,
                        PizzaId = 2,
                        SizeId = 3,
                        ExtraToppingIds = new List<int> {4}
                    }
                }
            };
            _result = subject.SaveOrder(saveOrderRequest);
        }

        [Test]
        public void ThenDatabaseSaveBasketIsCalledWithCorrectlyMappedDeliveryDate()
        {
            _database.Verify(x => x.Save(It.Is<BasketRecord>(y => y.DeliveryType == "Collection")), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveBasketIsCalledWithCorrectlyMappedStatus()
        {
            _database.Verify(x => x.Save(It.Is<BasketRecord>(y => y.Status == "Complete")), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveBasketIsCalledWithCorrectlyMappedTotal()
        {
            _database.Verify(x => x.Save(It.Is<BasketRecord>(y => y.Total == 1200)), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveBasketIsCalledWithCorrectlyMappedUser()
        {
            _database.Verify(x => x.Save(It.Is<BasketRecord>(y => y.User.Id == 1)), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveBasketIsCalledWithCorrectlyMappedVoucher()
        {
            _database.Verify(x => x.Save(It.Is<BasketRecord>(y => y.Voucher == "SOME_VOUCHER")), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveOrderIsCalledWithCorrectlyMappedBasket()
        {
            _database.Verify(x => x.Save(It.Is<OrderRecord>(y => y.Basket.Id == 1)), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveOrderIsCalledWithCorrectlyMappedPizza()
        {
            _database.Verify(x => x.Save(It.Is<OrderRecord>(y => y.Pizza.Id == 2)), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveOrderIsCalledWithCorrectlyMappedSize()
        {
            _database.Verify(x => x.Save(It.Is<OrderRecord>(y => y.Size.Id == 3)), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveOrderIsCalledWithCorrectlyMappedTotal()
        {
            _database.Verify(x => x.Save(It.Is<OrderRecord>(y => y.Total == 1000)), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveToppingIsCalledWithCorrectlyMappedOrder()
        {
            _database.Verify(x => x.Save(It.Is<OrderToppingRecord>(y => y.Order.Id == 2)), Times.Once);
        }

        [Test]
        public void ThenDatabaseSaveToppingIsCalledWithCorrectlyMappedTopping()
        {
            _database.Verify(x => x.Save(It.Is<OrderToppingRecord>(y => y.Topping.Id == 4)), Times.Once);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }
    }
}