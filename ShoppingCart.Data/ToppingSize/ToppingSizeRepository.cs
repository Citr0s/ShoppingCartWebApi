using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.ToppingSize
{
    public class ToppingSizeRepository : IToppingSizeRepository
    {
        private readonly IDatabase _database;

        public ToppingSizeRepository(IDatabase database)
        {
            _database = database;
        }

        public GetToppingSizeResponse GetByIds(List<int> extraToppingIds, int sizeId)
        {
            var response = new GetToppingSizeResponse();

            try
            {
                var toppingSizeRecords = _database.Query<ToppingSizeRecord>().Where(x => extraToppingIds.Contains(x.Topping.Id) && x.Size.Id == sizeId).ToList();

                if (toppingSizeRecords.Count == 0)
                {
                    response.AddError(new Error { Code = ErrorCodes.RecordNotFound, UserMessage = "Could not find matching ToppingSizeRecord." });
                    return response;
                }

                response.ToppingSize = toppingSizeRecords;
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Something went wrong when retrieving ToppingRecords from database."
                });
            }

            return response;
        }
    }
}