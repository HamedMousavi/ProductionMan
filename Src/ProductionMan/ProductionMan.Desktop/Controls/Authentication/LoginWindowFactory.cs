using System.Windows.Controls;
using ProductionMan.Desktop.Factories;

namespace ProductionMan.Desktop.Controls.Authentication
{

    public class LoginWindowFactory
    {
        
        private readonly CommandFactory _commandFactory;
        private readonly Domain.Security.User _user;


        public LoginWindowFactory(CommandFactory commandFactory, Domain.Security.User user)
        {
            _commandFactory = commandFactory;
            _user = user;
        }


        public UserControl CreateLoginPage()
        {
            return new Login
            {
                DataContext = new LoginViewModel
                {
                    LoginCommand = _commandFactory.CreateLoginCommand(_user),
                    ExitCommand = _commandFactory.CreateExitCommand()
                }
            };
        }


        public UserControl CreateLoginLoadinPage()
        {
            return new ProgressControl
            {
                DataContext = new ProgressControlViewModel
                {
                    Message = Localized.Resources.LoadinMessage,
                    ExitCommand = _commandFactory.CreateExitCommand()
                }
            };
        }


        public UserControl CreateLoginProgressPage()
        {
            return new ProgressControl
            {
                DataContext = new ProgressControlViewModel
                {
                    Message = Localized.Resources.SigningInMessage,
                    ExitCommand = _commandFactory.CreateExitCommand()
                }
            };
        }


        public UserControl CreateLoginStatusMessagePage()
        {
            return new StatusMessage
            {
                DataContext = new StatusMessageViewModel(_user)
                {
                    ExitCommand = _commandFactory.CreateExitCommand(),
                    RetryLoginCommand = _commandFactory.CreateRetryLoginCommand(_user)
                }
            };
        }
    }
}
