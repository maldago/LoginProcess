using System;
using System.Collections.Generic;

namespace Login
{
    public class LoginManager : ILoginManager
    {
        private Dictionary<string, string > _users;

        public LoginManager(Dictionary<string, string> users)
        {
            _users = users;
        }

        public LoginStatus Login(IUser user)
        {
            try
            {
				if (_users.ContainsKey(user.EmailAddress))
				{
					if (CryptographyController.EncryptPassword(user.Password).Equals(_users[user.EmailAddress]))
						return LoginStatus.Success;
					return LoginStatus.InvalidPassword;
				}
				return LoginStatus.UserDoesNotExist;
			}
            catch
            {
                return LoginStatus.Failed;
            }
        }
    }
}
