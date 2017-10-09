using System;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.PizzaPrice
{
    public class PizzaPriceRepository : IPizzaPriceRepository
    {
        private readonly IDatabase _database;

        public PizzaPriceRepository(IDatabase database)
        {
            _database = database;
        }

        public GetPizzaPricesResponse GetAll()
        {
            var response = new GetPizzaPricesResponse();

            try
            {
                response.PizzaPrices = _database.Query<PizzaPriceRecord>();
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