using ProductionMan.Common;
using System.Collections.Generic;


namespace ProductionMan.Domain.Security
{

    public class User : NotifyPropertyChanged
    {

        private LoginStates _loginStatus;
        private string _name;
        private string _culture;
        private long _id;


        public User()
        {
            _id = -1;
            _name = "Unknown User";
            _loginStatus = LoginStates.NeverSignedIn;
            Permissions = new List<Permission>();
        }


        public async void LoginAsync(string username, string password)
        {
            LoginStatus = LoginStates.SigningIn;

            ServiceProxy.SetCredentials(username, password);
            var permissions = await ServiceProxy.GetPermissions();

            LoginStatus = LoginStates.SignedIn;
        }


        public enum LoginStates
        {
            NeverSignedIn,
            SigningIn,
            SignedIn,
            IncorrectCredentials,
            Error,
        }


        public LoginStates LoginStatus
        {
            get { return _loginStatus; }
            private set
            {
                _loginStatus = value;
                FirePropertyChanged(this, "LoginStatus");
            }
        }


        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
                FirePropertyChanged(this, "Id");
            }
        }


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                FirePropertyChanged(this, "Name");
            }
        }


        public string Culture
        {
            get { return _culture; }
            set
            {
                _culture = value;
                FirePropertyChanged(this, "Culture");
            }
        }


        public List<Permission> Permissions { get; private set; }
    }
}
