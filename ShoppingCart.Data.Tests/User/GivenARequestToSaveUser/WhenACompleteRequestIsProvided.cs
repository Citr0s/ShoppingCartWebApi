using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Hasher;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.User;

namespace ShoppingCart.Data.Tests.User.GivenARequestToSaveUser
{
    [TestFixture]
    public class WhenACompleteRequestIsProvided
    {
        private SaveUserResponse _result;
        private Mock<IDatabase> _database;

        [OneTimeSetUp]
        public void SetUp()
        {
            _database = new Mock<IDatabase>();
            _database.Setup(x => x.Save(It.IsAny<UserRecord>()));
            _database.SetupSequence(x => x.Query<UserRecord>())
                .Returns(new List<UserRecord>())
                .Returns(new List<UserRecord>
                {
                    new UserRecord
                    {
                        Id = 1,
                        Email = "SOME_EMAIL_ADDRESS"
                    }
                });

            var hasher = new Mock<IHasher>();
            hasher.Setup(x => x.Hash(It.IsAny<string>())).Returns(() => "SOME_HASHED_PASSWORD");

            var subject = new UserRepository(_database.Object, hasher.Object);

            var request = new SaveUserRequest
            {
                Email = "SOME_EMAIL_ADDRESS",
                Password = "SOME_PASSWORD",
                PhoneNumber = "SOME_PHOME_NUMBER",
                Address = "SOME_ADDRESS"
            };
            _result = subject.Save(request);
        }

        [Test]
        public void ThenDatabaseIsCalledWithCorrectlyMappedAddress()
        {
            _database.Verify(x => x.Save(It.Is<UserRecord>(y => y.Address == "SOME_ADDRESS")), Times.Once);
        }

        [Test]
        public void ThenDatabaseIsCalledWithCorrectlyMappedEmail()
        {
            _database.Verify(x => x.Save(It.Is<UserRecord>(y => y.Email == "SOME_EMAIL_ADDRESS")), Times.Once);
        }

        [Test]
        public void ThenDatabaseIsCalledWithCorrectlyMappedPassword()
        {
            _database.Verify(x => x.Save(It.Is<UserRecord>(y => y.Password == "SOME_HASHED_PASSWORD")), Times.Once);
        }

        [Test]
        public void ThenDatabaseIsCalledWithCorrectlyMappedPhoneNumber()
        {
            _database.Verify(x => x.Save(It.Is<UserRecord>(y => y.PhoneNumber == "SOME_PHOME_NUMBER")), Times.Once);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenUserIdIsReturnedInTheResponse()
        {
            Assert.That(_result.UserId, Is.EqualTo(1));
        }
    }
}