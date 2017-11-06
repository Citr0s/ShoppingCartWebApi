using System;
using System.Linq;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Hasher;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabase _database;

        public UserRepository() : this(new NhibernateDatabase()) { }

        public UserRepository(IDatabase database)
        {
            _database = database;
        }

        public GetUserResponse GetByEmail()
        {
            var response = new GetUserResponse();

            try
            {
                response.User = _database.Query<UserRecord>().FirstOrDefault();
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving UserRecords from database."
                });
            }

            return response;
        }

        public SaveOrUpdateResponse SaveOrUpdate(SaveOrUpdateRequest request)
        {
            var response = new SaveOrUpdateResponse();

            try
            {
                var userRecord = new UserRecord
                {
                    Email = request.Email,
                    Password = Hasher.Hash(request.Password)
                };
                _database.Save(userRecord);
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Message = "Something went wrong when retrieving UserRecords from database."
                });
            }

            return response;
        }
    }
}