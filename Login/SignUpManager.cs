using System.Threading.Tasks;

namespace Login {

    public class SignUpManager : ISignUpManager
    {
        private IRegistrationManager _registrationManager;
        private ILoginManager _loginManager;
        private IConfigurationManager _configurationManager;
        private string _dataFile;

        public SignUpManager(ILoginManager loginManager, IRegistrationManager registrationManager, IConfigurationManager configurationManager, string dataFile)
        {
            _loginManager = loginManager;
            _registrationManager = registrationManager;
			_configurationManager = configurationManager;
			_dataFile = dataFile;
        }

        public LoginStatus Login(IUser user)
        {
            var result = _registrationManager.CheckUserExists(user);
            if(result == RegistrationStatus.UserExists)
                return _loginManager.Login(user);
            return LoginStatus.UserDoesNotExist;

        }

        public RegistrationStatus Register(IUser user)
        {
            var registrationResult = _registrationManager.Register(user);
            if (registrationResult == RegistrationStatus.Success)
                _configurationManager.SaveUsersAsync(_registrationManager.Users);
            return registrationResult;

        }
    }
}