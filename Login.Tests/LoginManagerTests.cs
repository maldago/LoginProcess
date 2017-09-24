using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Login.Tests
{
	[TestFixture]
    public class LoginManagerTests
    {
        private IUser _userSubstitute;
        private ILoginManager _loginManager;

        [SetUp]
        public void SetUp()
        {
            Dictionary<string, string> users = new Dictionary<string, string>
            {
                { "bob@bob.com", CryptographyController.EncryptPassword("bobLikesThaiFood") },
                { "stuart@bob.com", CryptographyController.EncryptPassword("stuartLikesThaiFood") },
                { "kevin@bob.com", CryptographyController.EncryptPassword("kevinLikesThaiFood") }
            };
            _loginManager = new LoginManager(users);
        }

        [Test]
        public void LoginUser_GivenUserWrongPassword_ResturnsInvalidPasswordLoginStatus()
        {
			//Given
			_userSubstitute = Substitute.For<IUser>();
			_userSubstitute.EmailAddress.Returns("bob@bob.com");
			_userSubstitute.Password.Returns("bobLikeThaiFood");

            var result = _loginManager.Login(_userSubstitute);

			Assert.AreEqual(LoginStatus.InvalidPassword, result);
        }

		[Test]
		public void LoginUser_GivenValidUser_ResturnsInvalidPasswordLoginStatus()
		{
			//Given
			_userSubstitute = Substitute.For<IUser>();
			_userSubstitute.EmailAddress.Returns("bob@bob.com");
			_userSubstitute.Password.Returns("bobLikesThaiFood");

			var result = _loginManager.Login(_userSubstitute);

			Assert.AreEqual(LoginStatus.Success, result);
		}

		[Test]
		public void LoginUser_GivenInvalidValidUser_ResturnsInvalidPasswordLoginStatus()
		{
			//Given
			_userSubstitute = Substitute.For<IUser>();
			_userSubstitute.EmailAddress.Returns("sarah@bob.com");
			_userSubstitute.Password.Returns("bobLikesThaiFood");

			var result = _loginManager.Login(_userSubstitute);

			Assert.AreEqual(LoginStatus.UserDoesNotExist, result);
		}

		[Test]
		public void LoginUser_GivenInvalidBrokenUser_ReturnsFailedLoginStatus()
		{
			//Given
			_userSubstitute = Substitute.For<IUser>();
			_userSubstitute.EmailAddress.Returns("bob@bob.com");
			_userSubstitute.Password.Returns(default(string));

			var result = _loginManager.Login(_userSubstitute);

			Assert.AreEqual(LoginStatus.Failed, result);
		}
    }
}
