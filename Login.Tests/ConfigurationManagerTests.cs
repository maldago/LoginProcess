using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Login.Tests
{
    [TestFixture]
    public class ConfigurationManagerTests
    {
        private IConfigurationManager _configurationManager;

        public ConfigurationManagerTests()
        {
        }

        [SetUp]
        public void Initialize()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "user.dat";
            _configurationManager = new ConfigurationManager(filePath);
        }

        [Test]
        public async Task ConfigurationManagerGetUsersNewFile()
        {
			var users = await _configurationManager.GetUsersAsync();
			Assert.IsInstanceOf(typeof(IDictionary<string, string>), users);
        }

        [Test]
		public void ConfigurationManagerSaveUsersNewFile()
		{
            var users = new Dictionary<string, string>();
            users.Add("bob@bob.com", CryptographyController.EncryptPassword("bobLikesThaiFood") );
            Assert.DoesNotThrowAsync(async () =>  await _configurationManager.SaveUsersAsync(users), "Failed", null);
		}

		[Test]
		public async Task ConfigurationManagerGetUsers()
		{
			var users = new Dictionary<string, string>();
			users.Add("bob@bob.com", CryptographyController.EncryptPassword("bobLikesThaiFood"));
            await _configurationManager.SaveUsersAsync(users);
			users = _configurationManager.GetUsersAsync().Result;
   
			Assert.AreEqual(1, users.Count());
            Assert.AreEqual("bob@bob.com", users.First().Key);
            Assert.AreEqual(CryptographyController.EncryptPassword("bobLikesThaiFood"), users.First().Value);
		}

        [OneTimeTearDown]
        public void TearDown()
        {
			string filePath = AppDomain.CurrentDomain.BaseDirectory + "user.dat";
            File.Delete(filePath);
        }
    }
}
