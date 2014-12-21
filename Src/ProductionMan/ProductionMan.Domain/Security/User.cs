using System.Net;
using ProductionMan.Common;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;
using System;
using System.Collections.Generic;


namespace ProductionMan.Domain.Security
{

    public class User : NotifyPropertyChanged
    {

        private LoginStates _loginStatus;
        private string _name;
        private string _culture;
        private long _id;
        private readonly Membership _membership;
        private string _loginStatusMessage;


        public User()
        {
            _id = -1;
            _name = "Unknown User";
            _loginStatus = LoginStates.NeverSignedIn;
            _membership = new Membership();
            Permissions = new List<Permission>();
        }


        public async void LoginAsync(string username, string password)
        {
            if (LoginStatus != LoginStates.NeverSignedIn)
            {
                throw new InvalidOperationException("Cannot login again while an incomplete login is in progress. Try calling RequestRetry() prior to calling this method.");
            }

            LoginStatus = LoginStates.SigningIn;

            _membership.SetCredentials(username, password);
            var response = await _membership.GetUserDetails();
            LoginStatusMessage = response.CallStatusMessage;
            LoginStatus = Map(response.CallStatusCode);
            Name = response.Results.Name;
            //var response = await _membership.GetPermissions();

            //Permissions = response.Results;
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


        public List<Permission> Permissions { get; private set; }
    }
}
