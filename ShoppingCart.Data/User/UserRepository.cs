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

        public GetUserResponse GetByEmail(string email, string password)
        {
            var response = new GetUserResponse();

            try
            {
                response.User = _database.Query<UserRecord>().First(x => x.Email == email && x.Password == Hasher.Hash(password));
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

        public SaveUserResponse Save(SaveOrUpdateRequest request)
        {
            var response = new SaveUserResponse();

            try
            {
                var userRecord = new UserRecord
                {
                    Email = request.Email,
                    Password = Hasher.Hash(request.Password)
                };
                _database.Save(userRecord);

                var userData = _database.Query<UserRecord>().First(x => x.Email == request.Email);
                response.UserId = userData.Id;
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