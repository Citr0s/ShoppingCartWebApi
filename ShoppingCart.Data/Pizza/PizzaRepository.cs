using System;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.Pizza
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly IDatabase _database;

        public PizzaRepository(IDatabase database)
        {
            _database = database;
        }

        public GetPizzasResponse GetAll()
        {
            var response = new GetPizzasResponse();

            try
            {
                response.Pizzas = _database.Query<PizzaRecord>();
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    TechnicalMessage = "Something went wrong when retrieving PizzaRecords from database."
                });
            }

            return response;
        }
    }
}