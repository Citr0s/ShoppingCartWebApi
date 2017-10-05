using System.Collections.Generic;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.Pizza
{
    public class GetGetPizzaRepository : IGetPizzaRepository
    {
        private readonly IDatabase _database;

        public GetGetPizzaRepository(IDatabase database)
        {
            _database = database;
        }

        public List<PizzaRecord> Get()
        {
            return _database.Select<PizzaRecord>("pizzas");
        }
    }
}