using System.Linq;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDatabase _database;

        public OrderRepository() : this(new NhibernateDatabase()) { }

        public OrderRepository(IDatabase database)
        {
            _database = database;
        }

        public SaveOrderResponse SaveOrder(SaveOrderRequest request)
        {
            var basketRecord = new BasketRecord
            {
                DeliveryType = request.DeliveryType,
                Total = request.GrandTotal,
                Voucher = request.Voucher,
                User = _database.Query<UserRecord>().First(x => x.Id == request.UserId)
            };
            var basketId = _database.Save(basketRecord).Id;

            _database.Save(new OrderRecord());

            // save baskets

            // save basketToppings

            return new SaveOrderResponse();
        }
    }
}
