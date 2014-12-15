using System;
using System.ComponentModel;
using ProductionMan.Common;
using ProductionMan.Domain.Security;


namespace ProductionMan.Desktop
{

    public class WindowManager
    {

        private readonly CommandFactory _commandFactory;


        public WindowManager(CommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }


        public void DisplayLoginWindow(User user)
        {
            // register for user changes so to load main window if login succeeded
            user.PropertyChanged -= UserOnPropertyChanged;
            user.PropertyChanged += UserOnPropertyChanged;

            // Display window
            var model = new LoginViewModel {LoginCommand = _commandFactory.CreateLoginCommand(user)};
            var window = new LoginWindow {DataContext = model};
            window.Show();
        }


        private void UserOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("LoginStatus"))
            {
                var user = (User) sender;
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
            // Undone:
        }
    }
}
