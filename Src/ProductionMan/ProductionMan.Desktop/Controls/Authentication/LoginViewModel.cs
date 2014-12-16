using System.Windows.Input;
using ProductionMan.Common;


namespace ProductionMan.Desktop.Controls.Authentication
{

    public class LoginViewModel : NotifyPropertyChanged
    {

        private string _username;
        private string _password;


        public ICommand LoginCommand { get; set; }


        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                FirePropertyChanged(this, "Username");
            }
        }


        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                FirePropertyChanged(this, "Password");
            }
        }
    }
}