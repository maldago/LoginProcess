using System;
namespace Login
{
    public class User : IUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Login.User"/> class.
        /// </summary>
        public User()
        {

        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
