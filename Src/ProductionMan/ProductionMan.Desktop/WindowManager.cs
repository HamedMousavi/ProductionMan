using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ProductionMan.Common;
using ProductionMan.Desktop.Controls;
using ProductionMan.Desktop.Controls.Authentication;
using ProductionMan.Domain.Security;


namespace ProductionMan.Desktop
{

    public class WindowManager
    {

        private readonly CommandFactory _commandFactory;
        private Window _loginWindow;


        public WindowManager(CommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }


        public void DisplayLoginWindow(User user)
        {
            // register for user changes so to load main window if login succeeded
            WireupEvent(user);

            // Create login content pages
            // Create a selector to select propert content based on each possible state
            var contentSelector = CreateLoginPageSelector(
                CreateLoginPage(user),
                CreateLoginStatusMessagePage(user),
                CreateLoginProgressPage(),
                CreateLoginLoadinPage());

            // Display window
            CreateLoginWindow(user, contentSelector).Show();
        }


        #region Login
       
        
        private Window CreateLoginWindow(User user, LoginWindowContentSelector contentSelector)
        {
            // Prepare view model
            var model = new LoginWindowViewModel { User = user, ActiveContentSelector = contentSelector };
            _loginWindow = new LoginWindow { DataContext = model };
            return _loginWindow;
        }


        private void WireupEvent(User user)
        {
            user.PropertyChanged -= UserOnPropertyChanged;
            user.PropertyChanged += UserOnPropertyChanged;
        }


        private LoginWindowContentSelector CreateLoginPageSelector(UserControl loginContent, UserControl errorContent, UserControl signingInContent, UserControl signedInContent)
        {
            var contentSelector = new LoginWindowContentSelector();
            contentSelector.AddContent(User.LoginStates.NeverSignedIn, loginContent);
            contentSelector.AddContent(User.LoginStates.IncorrectCredentials, errorContent);
            contentSelector.AddContent(User.LoginStates.Error, errorContent);
            contentSelector.AddContent(User.LoginStates.SigningIn, signingInContent);
            contentSelector.AddContent(User.LoginStates.SignedIn, signedInContent);

            return contentSelector;
        }


        private UserControl CreateLoginPage(User user)
        {
            return new Login
            {
                DataContext = new LoginViewModel
                {
                    LoginCommand = _commandFactory.CreateLoginCommand(user),
                    ExitCommand = _commandFactory.CreateExitCommand()
                }
            };
        }


        private UserControl CreateLoginLoadinPage()
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


        private UserControl CreateLoginProgressPage()
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


        private UserControl CreateLoginStatusMessagePage(User user)
        {
            return new StatusMessage
            {
                DataContext = new StatusMessageViewModel(user)
                {
                    ExitCommand = _commandFactory.CreateExitCommand(),
                    RetryLoginCommand = _commandFactory.CreateRetryLoginCommand(user)
                }
            };
        }


        private void UserOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("LoginStatus"))
            {
                var user = sender as User;
                if (user != null)
                {
                    if (user.LoginStatus == User.LoginStates.SignedIn)
                    {
                        DisplayMainWindow(user);
                    }
                }
            }
        }
        
        
        #endregion Login


        private void DisplayMainWindow(User user)
        {
            var wnd = new MainWindow
            {
                DataContext = new MainWindowViewModel
                {
                    Username = user.Name
                }
            };

            wnd.Show();

            _loginWindow.Close();
        }
    }
}
