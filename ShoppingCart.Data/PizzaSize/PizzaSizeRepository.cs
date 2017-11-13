using System;
using System.Linq;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
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
                    Code = ErrorCodes.DatabaseError,
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
                var pizzaSizeRecord = _database.Query<PizzaSizeRecord>().FirstOrDefault(x => x.Pizza.Id == pizzaId && x.Size.Id == sizeId);

                if (pizzaSizeRecord == null)
                {
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.RecordNotFound,
                        Message = "Could not find PizzaSizeRecords matching provided criteria"
                    });
                    return response;
                }

                response.PizzaSize = pizzaSizeRecord;
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    Message = "Something went wrong when retrieving PizzaPriceRecord from database."
                });
            }

            return response;
        }
    }
}