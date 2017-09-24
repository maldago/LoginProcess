using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Login.Tests
{
    [TestFixture]
    public class RegistrationManagerTests
    {
        private IRegistrationManager _registrationManager;

        [SetUp]
        public void Setup()
        {
			Dictionary<string, string> users = new Dictionary<string, string>
            {
				{ "bob@bob.com", CryptographyController.EncryptPassword("bobLikesThaiFood") },
				{ "stuart@bob.com", CryptographyController.EncryptPassword("stuartLikesThaiFood") },
				{ "kevin@bob.com", CryptographyController.EncryptPassword("kevinLikesThaiFood") }
			};

            _registrationManager = new RegistrationManager(users);
        }

        [Test]
        public void RegisterUser_GivenNewUser_RegistrationStatusSuccess()
        {
            var user =  Substitute.For<IUser>();
            user.EmailAddress = "steve@bob.com";
            user.Password = "steveLikeThaiFood";

            var result = _registrationManager.Register(user);

            Assert.AreEqual(RegistrationStatus.Success, result);
        }

		[Test]
		public void RegisterUser_GivenExistingUser_RegistrationStatusUserExists()
		{
			var user = Substitute.For<IUser>();
			user.EmailAddress = "bob@bob.com";
			user.Password = "bobLikesThaiFood";

			var result = _registrationManager.Register(user);

			Assert.AreEqual(RegistrationStatus.UserExists, result);
		}

		[Test]
		public void RegisterUser_GivenInvalidUser_RegistrationStatusFailed()
		{
			var user = Substitute.For<IUser>();
			user.EmailAddress = "azra@bob.com";
			user.Password = null;

			var result = _registrationManager.Register(user);

			Assert.AreEqual(RegistrationStatus.Failed, result);
		}

		[Test]
		public void CheckExisting_GivenValidUser_RegistrationStatusNewUser()
		{
			var user = Substitute.For<IUser>();
			user.EmailAddress = "azra@bob.com";
			user.Password = "azraLikeThaiFood";

			var result = _registrationManager.CheckUserExists(user);

            Assert.AreEqual(RegistrationStatus.NewUser, result);
		}

		[Test]
		public void CheckExisting_GivenInvalidUser_RegistrationStatusNewUser()
		{
			var user = Substitute.For<IUser>();
			user.EmailAddress = "beef@bob.com";
			user.Password = "beefLikeThaiFood";

			var result = _registrationManager.CheckUserExists(user);

			Assert.AreEqual(RegistrationStatus.NewUser, result);
		}
    }
}
