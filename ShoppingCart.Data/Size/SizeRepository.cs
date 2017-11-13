using System;
using ShoppingCart.Core.Communication;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.Size
{
    public class SizeRepository : ISizeRepository
    {
        private readonly IDatabase _database;

        public SizeRepository(IDatabase database)
        {
            _database = database;
        }

        public GetSizesResponse GetAll()
        {
            var response = new GetSizesResponse();

            try
            {
                response.Sizes = _database.Query<SizeRecord>();
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    TechnicalMessage = "Something went wrong when retrieving SizeRecords from database."
                });
            }

            return response;
        }
    }
}