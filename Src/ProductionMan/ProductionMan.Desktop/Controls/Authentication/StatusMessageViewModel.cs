using System.ComponentModel;
using System.Windows.Input;
using ProductionMan.Common;
using ProductionMan.Domain.Security;

namespace ProductionMan.Desktop.Controls.Authentication
{

    public class StatusMessageViewModel : NotifyPropertyChanged
    {
        
        private User.LoginStates _loginStatus;
        private string _loginStatusMessage;
        private ICommand _exitCommand;
        private ICommand _retryLoginCommand;


        // UNDONE: IS violation: Fat interface, try a better parameter
        public StatusMessageViewModel(User user)
        {
            user.PropertyChanged += UserOnPropertyChanged;
        }


        private void UserOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("LoginStatus"))
            {
                var user = sender as User;
                if (user != null)
                {
                    LoginStatus = user.LoginStatus;
                    LoginStatusMessage = user.LoginStatusMessage;
                }
            }
        }


        public User.LoginStates LoginStatus
        {
            get { return _loginStatus; }
            set
            {
                _loginStatus = value;
                FirePropertyChanged(this, "LoginStatus");
            }
        }


        public string LoginStatusMessage
        {
            get { return _loginStatusMessage; }
            set
            {
                _loginStatusMessage = value;
                FirePropertyChanged(this, "LoginStatusMessage");
            }
        }


        public ICommand RetryLoginCommand
        {
            get { return _retryLoginCommand; }
            set
            {
                _retryLoginCommand = value;
                FirePropertyChanged(this, "RetryLoginCommand");
            }
        }


        public ICommand ExitCommand
        {
            get { return _exitCommand; }
            set
            {
                _exitCommand = value;
                FirePropertyChanged(this, "ExitCommand");
            }
        }
    }
}