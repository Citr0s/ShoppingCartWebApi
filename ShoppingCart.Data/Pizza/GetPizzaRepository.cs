using System;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.Pizza
{
    public class GetPizzaRepository : IGetPizzaRepository
    {
        private readonly IDatabase _database;

        public GetPizzaRepository(IDatabase database)
        {
            _database = database;
        }

        public GetPizzasResponse GetAll()
        {
            var response = new GetPizzasResponse();

            try
            {
                response.Pizzas = _database.Select<PizzaRecord>();
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving PizzaRecords from database."
                });
            }

            return response;
        }
    }
}