using System.Collections.Generic;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.Pizza
{
    public class GetPizzaRepository
    {
        private readonly IDatabase _database;

        public GetPizzaRepository(IDatabase database)
        {
            _database = database;
        }

        public List<PizzaRecord> Get()
        {
            return _database.Select<PizzaRecord>("pizzas");
        }
    }
}