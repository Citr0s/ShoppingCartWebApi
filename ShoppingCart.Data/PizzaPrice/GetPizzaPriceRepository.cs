using System;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.PizzaPrice
{
    public class GetPizzaPriceRepository : IGetPizzaPriceRepository
    {
        private readonly IDatabase _database;

        public GetPizzaPriceRepository(IDatabase database)
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