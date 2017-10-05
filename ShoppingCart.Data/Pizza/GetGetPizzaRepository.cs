using System;
using ShoppingCart.Core.Communication;
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

        public GetPizzasResponse GetAll()
        {
            var response = new GetPizzasResponse();

            // TODO: REMEMBER TO ADD TESTS WHEN DATABASE BLOWS UP!!!!!
            try
            {
                response.Pizzas = _database.Select<PizzaRecord>("pizzas");
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