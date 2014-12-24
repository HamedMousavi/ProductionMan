using System.Net;
using System.Threading.Tasks;
using ProductionMan.Common;
using ProductionMan.Desktop.Commands;
using ProductionMan.Desktop.Controls;
using ProductionMan.Desktop.Controls.Authentication;
using ProductionMan.Desktop.Controls.MainParts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.WebServices;


namespace ProductionMan.Desktop
{

    public class WindowManager : IUserWindowManager
    {

        private readonly CommandFactory _commandFactory;
        private readonly Membership _membershipService;
        private readonly ILanguageService _languageService;
        private readonly IStatusService _statusService;
        private Window _loginWindow;


        public WindowManager(CommandFactory commandFactory, Membership membershipService, ILanguageService languageService, IStatusService statusService)
        {
            _commandFactory = commandFactory;
            _membershipService = membershipService;
            _languageService = languageService;
            _statusService = statusService;
        }


        public void DisplayLoginWindow(Domain.Security.User user)
        {
            // register for user changes so to load main window if login succeeded
            WireupEvent(user);

            var windowSelector = 
                new LoginWindowSelector(
                    new LoginWindowFactory(_commandFactory, user));

            // Display window
            CreateLoginWindow(user, windowSelector).Show();
        }


        #region Login


        private Window CreateLoginWindow(Domain.Security.User user, BaseContentSelector<Domain.Security.User.LoginStates> contentSelector)
        {
            // Prepare view model
            var model = new LoginWindowViewModel { User = user, ActiveContentSelector = contentSelector };
            _loginWindow = new LoginWindow { DataContext = model };
            return _loginWindow;
        }


        private void WireupEvent(Domain.Security.User user)
        {
            user.PropertyChanged -= UserOnPropertyChanged;
            user.PropertyChanged += UserOnPropertyChanged;
        }


        private async void UserOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("LoginStatus"))
            {
                var user = sender as Domain.Security.User;
                if (user != null)
                {
                    if (user.LoginStatus == Domain.Security.User.LoginStates.SignedIn)
                    {
                        await DisplayMainWindow(user);
                        _loginWindow.Close();
                    }
                }
            }
        }
        
        
        #endregion Login


        private async Task DisplayMainWindow(Domain.Security.User user)
        {
            var windowSelector = 
                new MainWindowSelector(_membershipService,
                    new MainWindowFactory(_membershipService, _commandFactory, _languageService, this));

            var result = await windowSelector.CreateContent();

            if (result.CallStatusCode != HttpStatusCode.OK) return;

            CreateMainWindow(
                windowSelector,
                new LogonBoxViewModel { User = user },
                windowSelector.Tabs).Show();
        }


        private Window CreateMainWindow(MainWindowSelector windowSelector, LogonBoxViewModel logonBoxViewModel, ObservableCollection<TabItemViewModel> tabsViewModel)
        {
            var wnd = new MainWindow
            {
                DataContext = new MainWindowViewModel(_statusService)
                {
                    Tabs = tabsViewModel,
                    LogonBoxModel = logonBoxViewModel,
                    ActiveContentSelector = windowSelector,
                    SelectedItem = tabsViewModel[0]
                }
            };

            return wnd;
        }

        public void DisplayUserEditorWindow(UserEditorWindowViewModel viewModel)
        {
            
        }

        public bool RequestPermissionToDelete(UserEditorWindowViewModel viewModel)
        {
            return true;
        }

        public void DisplayUserAddWindow(UserEditorWindowViewModel viewModel)
        {
            
        }
    }
}
