using System.Threading.Tasks;

namespace Login {

    public class SignUpManager : ISignUpManager
    {
        private IRegistrationManager _registrationManager;
        private ILoginManager _loginManager;
        private object _configurationManager;
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
            return _loginManager.Login(user);

        }

        public RegistrationStatus Register(IUser user)
        {
            return _registrationManager.Register(user);
        }
    }
}