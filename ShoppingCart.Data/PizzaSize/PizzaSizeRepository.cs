using System;
using System.Linq;
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
                response.PizzaSizes = _database.Query<PizzaSizeRecord>();
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

        public GetPizzaSizeResponse GetByIds(int pizzaId, int sizeId)
        {
            var response = new GetPizzaSizeResponse();

            try
            {
                response.PizzaSize = _database.Query<PizzaSizeRecord>().First(x => x.Pizza.Id == pizzaId && x.Size.Id == sizeId);
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving PizzaPriceRecord from database."
                });
            }

            return response;
        }
    }
}