using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Hasher;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Tests.User.GivenARequestToGetUserByEmailAddress
{
    [TestFixture]
    public class WhenEmailAndPasswordHaveBeenFoundInTheDatabase
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
                    Email = "SOME_EMAIL",
                    Password = "SOME_HASHED_PASSWORD"
                }
            });

            var hasher = new Mock<IHasher>();
            hasher.Setup(x => x.Hash(It.IsAny<string>())).Returns(() => "SOME_HASHED_PASSWORD");

            var subject = new UserRepository(database.Object, hasher.Object);
            _result = subject.GetByEmail("SOME_EMAIL", "SOME_PASSWORD");
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenTheUserIsReturned()
        {
            Assert.That(_result.User.Id, Is.EqualTo(1));
        }
    }
}
