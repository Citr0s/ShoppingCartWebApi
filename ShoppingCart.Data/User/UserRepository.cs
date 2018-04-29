﻿using System;
using System.Linq;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Core.Hasher;
using ShoppingCart.Data.Database;

namespace ShoppingCart.Data.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabase _database;
        private readonly IHasher _hasher;

        public UserRepository(IDatabase database, IHasher hasher)
        {
            _database = database;
            _hasher = hasher;
        }

        public GetUserResponse GetByEmail(string email, string password)
        {
            var response = new GetUserResponse();

            try
            {
                var user = _database.Query<UserRecord>()
                    .FirstOrDefault(x => x.Email == email && x.Password == _hasher.Hash(password));

                if (user == null)
                {
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.UserNotFound,
                        UserMessage = "User with specified credentials could not be found"
                    });
                    return response;
                }

                response.User = user;
            }
            catch (Exception)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Something went wrong when retrieving UserRecords from database."
                });
            }

            return response;
        }

        public SaveUserResponse Save(SaveUserRequest request)
        {
            var response = new SaveUserResponse();

            try
            {
                var user = _database.Query<UserRecord>()
                    .FirstOrDefault(x => x.Email == request.Email);

                if (user != null)
                {
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.UserAlreadyRegistered,
                        UserMessage = "User with specified credentials already exists"
                    });
                    return response;
                }

                var userRecord = new UserRecord
                {
                    Email = request.Email,
                    Password = _hasher.Hash(request.Password),
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address
                };
                _database.Save(userRecord);

                var userData = _database.Query<UserRecord>().First(x => x.Email == request.Email);
                response.UserId = userData.Id;
            }
            catch (Exception exception)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Could not create account. Please try again later.",
                    TechnicalMessage = $"Something went wrong when retrieving UserRecords from database. The following exception was thrown: {exception}"
                });
            }

            return response;
        }
    }
}