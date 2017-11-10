using ShoppingCart.Data.Database;

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
            return new SaveOrderResponse();
        }
    }
}
