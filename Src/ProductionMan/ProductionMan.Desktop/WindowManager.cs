﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
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
            user.PropertyChanged -= UserOnPropertyChanged;
            user.PropertyChanged += UserOnPropertyChanged;

            // Create Login content
            var loginContent = new Login
            {
                DataContext = new LoginViewModel
                {
                    LoginCommand = _commandFactory.CreateLoginCommand(user),
                    ExitCommand = _commandFactory.CreateExitCommand()
                }
            };

            // Create content for other states in LoginWindow
            var signingInContent = new ProgressControl
            {
                DataContext = new ProgressControlViewModel {Message = Localized.Resources.SigningInMessage}
            };

            var signedInContent = new ProgressControl
            {
                DataContext = new ProgressControlViewModel {Message = Localized.Resources.LoadinMessage}
            };

            var errorContent = new StatusMessage
            {
                DataContext = new StatusMessageViewModel(user)
                {
                    ExitCommand = _commandFactory.CreateExitCommand(),
                    RetryLoginCommand = _commandFactory.CreateRetryLoginCommand(user)
                }
            };

            // Create a selector to select propert content based on each possible state
            var contentSelector = new LoginWindowContentSelector();
            contentSelector.AddContent(User.LoginStates.NeverSignedIn, loginContent);
            contentSelector.AddContent(User.LoginStates.IncorrectCredentials, errorContent);
            contentSelector.AddContent(User.LoginStates.Error, errorContent);
            contentSelector.AddContent(User.LoginStates.SigningIn, signingInContent);
            contentSelector.AddContent(User.LoginStates.SignedIn, signedInContent);

            // Prepare view model
            var model = new LoginWindowViewModel { User = user, ActiveContentSelector = contentSelector };

            // Display window
            (_loginWindow = new LoginWindow {DataContext = model}).Show();
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
