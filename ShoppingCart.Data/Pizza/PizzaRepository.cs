using System;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
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
            catch (Exception exception)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Something went wrong when retrieving PizzaRecords from database.",
                    TechnicalMessage = $"The following exception was thrown '{exception.Message}'"
                });
            }

            return response;
        }
    }
}