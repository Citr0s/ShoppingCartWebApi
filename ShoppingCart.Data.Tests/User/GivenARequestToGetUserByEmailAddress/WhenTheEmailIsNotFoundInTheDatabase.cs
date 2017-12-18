using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Core.Hasher;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Tests.User.GivenARequestToGetUserByEmailAddress
{
    [TestFixture]
    public class WhenTheEmailIsNotFoundInTheDatabase
    {
        private GetUserResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<UserRecord>()).Returns(() => new List<UserRecord>
            {
                new UserRecord
                {
                    Id = 1,
                    Email = "SOME_OTHER_EMAIL",
                    Password = "SOME_HASHED_PASSWORD"
                }
            });

            var subject = new UserRepository(database.Object, new Mock<IHasher>().Object);
            _result = subject.GetByEmail("SOME_EMAIL", "SOME_PASSWORD");
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenNoUsersAreReturned()
        {
            Assert.That(_result.User, Is.Null);
        }

        [Test]
        public void ThenTheCorrectErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.UserNotFound));
        }
    }
}