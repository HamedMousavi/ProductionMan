using System.Windows.Input;
using ProductionMan.Domain.Security;

namespace ProductionMan.Desktop.Commands
{
    public class LoginRetryCommand : ICommand
    {

        private readonly User _user;


        public LoginRetryCommand(User user)
        {
            _user = user;
        }


        public bool CanExecute(object parameter)
        {
            return _user != null && _user.LoginStatus != User.LoginStates.SigningIn;
        }


        public event System.EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            if (_user != null)
            {
                _user.RequestRetry();
            }
        }
    }
}
