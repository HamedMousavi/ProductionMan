using ProductionMan.Desktop.Commands;
using ProductionMan.Desktop.Controls.Authentication;
using ProductionMan.Desktop.Controls.MainParts;
using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Controls.MainParts.ControlFactories;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.Security;
using ProductionMan.Domain.WebServices;
using System.Collections.Generic;


namespace ProductionMan.Desktop.Factories
{

    public class ViewModelFactory
    {

        private readonly Dictionary<string, object> _viewModels;

        private readonly Membership _membershipService;
        private readonly CommandFactory _commandFactory;
        private readonly ILanguageService _languageService;
        private readonly WindowManager _windowManager;
        
        private readonly User _user;
        private readonly AppServicesFactory _appServiceFactory;
        private readonly DataFactory _dataFactory;


        public ViewModelFactory(Membership membershipService, CommandFactory commandFactory,
            ILanguageService languageService, WindowManager windowManager, User user, AppServicesFactory appServiceFactory, DataFactory dataFactory)
        {
            _viewModels = new Dictionary<string, object>();

            _membershipService = membershipService;
            _commandFactory = commandFactory;
            _languageService = languageService;
            _windowManager = windowManager;
            _user = user;
            _appServiceFactory = appServiceFactory;
            _dataFactory = dataFactory;
        }


        internal object CreateUserManagerViewModel()
        {
            if (!_viewModels.ContainsKey("Users"))
            {
                _viewModels.Add("Users", new UserManagerViewModel
                {
                    AddCommand = _commandFactory.CreateAddUserCommand(_windowManager, _membershipService, _languageService),
                    DeleteCommand = _commandFactory.CreateDeleteUserCommand(_windowManager, _membershipService),
                    EditCommand = _commandFactory.CreateEditUserCommand(_windowManager, _membershipService),
                    ToggleUserEnabledStatusCommand = _commandFactory.ToggleUserCommand(),
                    Items = _dataFactory.Users
                });
            }

            return _viewModels["Users"];
        }


        internal TabItemViewModel CreateUserManagerTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitleUsers,
                HeaderIcon = "User",
                PageTitle = Localized.Resources.PageTitleUsers,
                Toolbar = new GenericListToolbar { DataContext = CreateUserManagerViewModel() }
            };
        }


        internal SettingsPageViewModel CreateSettingsViewModel()
        {
            return new SettingsPageViewModel(_languageService);
        }


        internal AboutPageViewModel CreateAboutViewModel()
        {
            return new AboutPageViewModel { OpenUrlCommand = new NavigateToWebsiteCommand() };
        }


        internal LoginWindowViewModel CreateLoginWindowViewModel()
        {
            var windowSelector =
                new LoginWindowSelector(
                    new LoginWindowFactory(_commandFactory, _user));

            return new LoginWindowViewModel { User = _user, ActiveContentSelector = windowSelector };
        }


        internal MainWindowViewModel CreateMainWindowViewModel()
        {
            var contentSelector = new MainWindowSelector(this);
            contentSelector.CreateContent(_dataFactory.Permissions);
            return new MainWindowViewModel(_appServiceFactory.CreateStatusService())
            {
                Tabs = contentSelector.Tabs,
                LogonBoxModel = CreateLogonBoxViewModel(),
                ActiveContentSelector = contentSelector,
                SelectedItem = contentSelector.Tabs[0]
            };
        }


        private LogonBoxViewModel CreateLogonBoxViewModel()
        {
            return new LogonBoxViewModel {User = _user};
        }
    }
}


            //HeaderLabel = Localized.Resources.TabTitleStores,
            //HeaderIcon = "Stores",
            //PageTitle = Localized.Resources.PageTitleStores,
            //Toolbar = new GenericListToolbar { DataContext = _viewModel }
                //HeaderLabel = Localized.Resources.TabTitleProcesses,
                //HeaderIcon = "Process",
                //PageTitle = Localized.Resources.PageTitleProcesses,
                ////Toolbar = new GenericListToolbar { DataContext = _viewModel }
        //HeaderLabel = Localized.Resources.TabTitlePermissions,
        //HeaderIcon = "Permission",
        //PageTitle = Localized.Resources.PageTitlePermissions,
        ////Toolbar = new GenericListToolbar { DataContext = _viewModel }
                //HeaderLabel = Localized.Resources.TabTitleMaterials,
                //HeaderIcon = "Package",
                //PageTitle = Localized.Resources.PageTitleMaterials,
                ////Toolbar = new GenericListToolbar { DataContext = _viewModel }
