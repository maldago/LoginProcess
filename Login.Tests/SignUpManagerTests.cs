using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Login.Tests
{
    [TestFixture]
    public class SignUpManagerTests
    {
        private SignUpManager _signupManager;
        private ConfigurationManager _configurationManager;
        private LoginManager _loginManager;
        private RegistrationManager _registrationManager;

        [SetUp]
        public void Setup()
        {

            string filePath = AppDomain.CurrentDomain.BaseDirectory + "user.dat";
            _configurationManager = Substitute.For<ConfigurationManager>(filePath);
			var users = new Dictionary<string, string>
			{
				{ "bob@bob.com", CryptographyController.EncryptPassword("bobLikesThaiFood") },
				{ "stuart@bob.com", CryptographyController.EncryptPassword("stuartLikesThaiFood") },
				{ "kevin@bob.com", CryptographyController.EncryptPassword("kevinLikesThaiFood") }
            };
          
            _loginManager = Substitute.For<LoginManager>(users);
            _registrationManager = Substitute.For<RegistrationManager>(users);

            _signupManager = new SignUpManager(_loginManager, _registrationManager, _configurationManager, filePath);
        }

        [Test]
        public void SignUp_RegisterNewUser_SuccessResult()
        {
            var user = Substitute.For<IUser>();
            user.EmailAddress.Returns("gary@snail.com");
            user.Password.Returns("iLoveSpongeBob");

            var result = _signupManager.Register(user);

            Received.InOrder(async () => await _configurationManager.SaveUsersAsync(Arg.Any<Dictionary<string, string>>()));

			var loginResult = _signupManager.Login(user);

			Assert.AreEqual(RegistrationStatus.Success, result);
            Assert.AreEqual(LoginStatus.Success, loginResult);
        }

		[Test]
		public void SignUp_RegisterExistingUser_SuccessResult()
		{
			var user = Substitute.For<IUser>();
			user.EmailAddress.Returns("bob@bob.com");
			user.Password.Returns("bobLikesThaiFood");

			var result = _signupManager.Register(user);
			Assert.AreEqual(RegistrationStatus.UserExists, result);
		}

		[Test]
		public void SignUp_RegisterInvalidUser_FailedResult()
		{
			var user = Substitute.For<IUser>();
            user.EmailAddress.Returns("alfonso@bob.com");
            user.Password = null;

			var result = _signupManager.Register(user);
			Assert.AreEqual(RegistrationStatus.Failed, result);
		}

        [Test]
        public void SignUp_LoginCheckIfRegistereed_LoginSuccess()
        {
			var user = Substitute.For<IUser>();
			user.EmailAddress.Returns("bob@bob.com");
			user.Password.Returns("bobLikesThaiFood");
          
           var result =   _signupManager.Login(user);
            _registrationManager.Received().CheckUserExists(user);
            Assert.AreEqual(LoginStatus.Success, result);
        }

		[Test]
		public void SignUp_LoginCheckIfRegistereed_LoginStatusUserDoesNotExist()
		{
			var user = Substitute.For<IUser>();
			user.EmailAddress.Returns("jerome@bob.com");
			user.Password.Returns("bobLikesThaiFood");

			var result = _signupManager.Login(user);
			_registrationManager.Received().CheckUserExists(user);
			Assert.AreEqual(LoginStatus.UserDoesNotExist, result);
		}
    }
}
