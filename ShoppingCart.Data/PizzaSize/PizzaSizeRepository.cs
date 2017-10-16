using System;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.PizzaSize
{
    public class PizzaSizeRepository : IPizzaSizeRepository
    {
        private readonly IDatabase _database;

        public PizzaSizeRepository(IDatabase database)
        {
            _database = database;
        }

        public GetPizzaSizesResponse GetAll()
        {
            var response = new GetPizzaSizesResponse();

            try
            {
                response.PizzaPrices = _database.Query<PizzaSizeRecord>();
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving PizzaPriceRecords from database."
                });
            }

            return response;
        }
    }
}