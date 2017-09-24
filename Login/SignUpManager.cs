using System.Threading.Tasks;

namespace Login {

    public class SignUpManager : ISignUpManager
    {
        private IRegistrationManager _registrationManager;
        private ILoginManager _loginManager;
        private IConfigurationManager _configurationManager;
        private string _dataFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Login.SignUpManager"/> class.
        /// </summary>
        /// <param name="loginManager">Login manager.</param>
        /// <param name="registrationManager">Registration manager.</param>
        /// <param name="configurationManager">Configuration manager.</param>
        /// <param name="dataFile">Data file.</param>
        public SignUpManager(ILoginManager loginManager, IRegistrationManager registrationManager, IConfigurationManager configurationManager, string dataFile)
        {
            _loginManager = loginManager;
            _registrationManager = registrationManager;
			_configurationManager = configurationManager;
			_dataFile = dataFile;
        }

        /// <summary>
        /// Login the specified user.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="user">User.</param>
        public LoginStatus Login(IUser user)
        {
            var result = _registrationManager.CheckUserExists(user);
            if(result == RegistrationStatus.UserExists)
                return _loginManager.Login(user);
            return LoginStatus.UserDoesNotExist;

        }

        /// <summary>
        /// Register the specified user.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="user">User.</param>
        public RegistrationStatus Register(IUser user)
        {
            var registrationResult = _registrationManager.Register(user);
            if (registrationResult == RegistrationStatus.Success)
                _configurationManager.SaveUsersAsync(_registrationManager.Users);
            return registrationResult;

        }
    }
}