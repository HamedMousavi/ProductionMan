using System;
using System.Net;
using System.Threading.Tasks;
using ProductionMan.Common;
using ProductionMan.Domain.WebServices;

namespace ProductionMan.Domain.Security
{

    public class User : NotifyPropertyChanged
    {

        private LoginStates _loginStatus;
        private string _name;
        private string _culture;
        private long _id;
        private readonly Membership _membershipService;
        private string _loginStatusMessage;


        public User(Membership membershipService)
        {
            _membershipService = membershipService;
            _id = -1;
            _name = "Unknown User";
            _loginStatus = LoginStates.NeverSignedIn;
            //_membershipService = new Membership();
            //Permissions = new List<Permission>();
        }


        public async Task LoginAsync(string username, string password)
        {
            if (LoginStatus != LoginStates.NeverSignedIn)
            {
                throw new InvalidOperationException("Cannot login again while an incomplete login is in progress. Try calling RequestRetry() prior to calling this method.");
            }

            // Set status to looging in
            LoginStatus = LoginStates.SigningIn;

            // Set credentials
            //if (_membershipService.ServiceCredentialProvider == null)
            //{
            // Reset it each time, user/pass might have changed!
                var provider = new DefaultServiceCredentialProvider();
                provider.SetCredentials(username, password);
                _membershipService.ServiceCredentialProvider = provider;
            //}

            var response = await _membershipService.GetUserProfile();

            // If signed in successfully
            if (response.CallStatusCode == HttpStatusCode.OK)
            {
                // Set users name
                Name = response.Results.DisplayName;
                
                //// Load list of user permissions
                //var perms = await _membershipService.GetPermissions();
                //Permissions = perms.Results;
            }

            // Update status based on login info
            LoginStatusMessage = response.CallStatusMessage;
            LoginStatus = Map(response.CallStatusCode);
        }


        private LoginStates Map(HttpStatusCode httpStatus)
        {
            switch (httpStatus)
            {
                case HttpStatusCode.OK:
                    return LoginStates.SignedIn;

                case HttpStatusCode.Unauthorized:
                    return LoginStates.IncorrectCredentials;

                default:
                    return LoginStates.Error;
            }
        }


        /// <summary>
        /// This call will prepare user for a second login attempt
        /// </summary>
        public void RequestRetry()
        {
            if (LoginStatus == LoginStates.SigningIn)
            {
                throw new InvalidOperationException("Cannot login again while an incomplete login is in progress. Wait before previous request is complete.");
            }

            LoginStatus = LoginStates.NeverSignedIn;
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


        public string LoginStatusMessage
        {
            get { return _loginStatusMessage; }
            private set
            {
                _loginStatusMessage = value;
                FirePropertyChanged(this, "LoginStatusMessage");
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


        //public List<Permission> Permissions { get; private set; }
    }
}
