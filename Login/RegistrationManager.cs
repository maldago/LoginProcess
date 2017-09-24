using System.Collections.Generic;

namespace Login
{
    /// <summary>
    /// Registration manager.
    /// </summary>
    public class RegistrationManager : IRegistrationManager
    {
        public Dictionary<string, string> Users { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Login.RegistrationManager"/> class.
        /// </summary>
        /// <param name="users">Users.</param>
        public RegistrationManager(Dictionary<string, string> users)
        {
            Users = users;
        }

        /// <summary>
        /// Register the specified user.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="user">User.</param>
        public RegistrationStatus Register(IUser user)
        {
            var status = CheckUserExists(user);
            if (status == RegistrationStatus.NewUser)
            {
                try
                {
                    string encrypterPassword = CryptographyController.EncryptPassword(user.Password);
                    Users.Add(user.EmailAddress, encrypterPassword);
                    status = RegistrationStatus.Success;
                }
                catch
                {
                    status = RegistrationStatus.Failed;
                }
            }
            return status;
        }

        /// <summary>
        /// Checks the user exists.
        /// </summary>
        /// <returns>The user exists.</returns>
        /// <param name="user">User.</param>
        public RegistrationStatus CheckUserExists(IUser user)
        {
            try
            {
                if (Users.ContainsKey(user.EmailAddress))
                    return RegistrationStatus.UserExists;
                return RegistrationStatus.NewUser;
            }
            catch
            {
                return RegistrationStatus.Failed;
            }
        }
    }
}
