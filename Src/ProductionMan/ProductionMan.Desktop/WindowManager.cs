using ProductionMan.Common;
using ProductionMan.Desktop.Commands;
using ProductionMan.Desktop.Controls;
using ProductionMan.Desktop.Controls.Authentication;
using ProductionMan.Desktop.Controls.MainParts;
using ProductionMan.Desktop.Controls.MainParts.ControlFactories;
using ProductionMan.Desktop.Repositories;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.Security;
using ProductionMan.Domain.WebServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;


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


        public void DisplayLoginWindow(User user)
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


        private Window CreateLoginWindow(User user, BaseContentSelector<User.LoginStates> contentSelector)
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


        private async void UserOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("LoginStatus"))
            {
                var user = sender as User;
                if (user != null)
                {
                    if (user.LoginStatus == User.LoginStates.SignedIn)
                    {
                        var data = await LoadData();
                        DisplayMainWindow(user, data);
                        _loginWindow.Close();
                    }
                }
            }
        }
        
        
        #endregion Login


        private async Task<MainWindowDataProvider> LoadData()
        {
            var dataProvider = 
                new MainWindowDataProvider(
                    new MembershipRepository(_membershipService));
            await dataProvider.LoadData();

            return dataProvider;
        }


        private void DisplayMainWindow(User user, MainWindowDataProvider dataProvider)
        {
            var windowSelector = 
                new MainWindowSelector(dataProvider,new MainWindowFactory(),
                    new ViewModelFactory(
                        _membershipService, 
                        _commandFactory, 
                        _languageService, 
                        this, 
                        dataProvider));

            windowSelector.CreateContent();

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
            new UserEditorWindow { DataContext = viewModel }.ShowDialog();
        }

        public void RequestPermissionToDelete(ConfirmDeleteWindowViewModel viewModel)
        {
            new ConfirmDeleteWindow {DataContext = viewModel}.ShowDialog();
        }

        public void DisplayUserAddWindow(UserEditorWindowViewModel viewModel)
        {
            new UserEditorWindow{DataContext = viewModel}.ShowDialog();
        }
    }
}
