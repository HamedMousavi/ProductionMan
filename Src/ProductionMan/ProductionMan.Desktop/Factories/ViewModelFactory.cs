using ProductionMan.Desktop.Commands;
using ProductionMan.Desktop.Controls.Authentication;
using ProductionMan.Desktop.Controls.MainParts;
using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.Security;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;


namespace ProductionMan.Desktop.Factories
{

    public class ViewModelFactory
    {

        private readonly Dictionary<string, object> _viewModels;

        private readonly CommandFactory _commandFactory;
        private readonly ILanguageService _languageService;

        private readonly User _user;
        private readonly AppServicesFactory _appServiceFactory;
        private readonly DataFactory _dataFactory;
        private const string UsersViewModelKey = "Users";
        private const string PermissionsViewModelKey = "Permissions";


        public ViewModelFactory(CommandFactory commandFactory,
            ILanguageService languageService, User user, AppServicesFactory appServiceFactory, DataFactory dataFactory)
        {
            _viewModels = new Dictionary<string, object>();

            _commandFactory = commandFactory;
            _languageService = languageService;
            _user = user;
            _appServiceFactory = appServiceFactory;
            _dataFactory = dataFactory;
        }


        internal object CreateUserManagerViewModel()
        {
            if (!_viewModels.ContainsKey(UsersViewModelKey))
            {
                _viewModels.Add(UsersViewModelKey, new UserManagerViewModel
                {
                    AddCommand = _commandFactory.AddUserWindowCommand(),
                    DeleteCommand = _commandFactory.DeleteUserConfirmWindowCommand(),
                    EditCommand = _commandFactory.EditUserWindowCommand(),
                    ToggleUserEnabledStatusCommand = _commandFactory.ToggleUserCommand(),
                    Items = _dataFactory.Users
                });
            }

            return _viewModels[UsersViewModelKey];
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
            return new AboutPageViewModel { OpenUrlCommand = _commandFactory.NavigateToWebsiteCommand() };
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


        public UserEditorWindowViewModel CreateUserEditViewModel(UserWrite user)
        {
            return new UserEditorWindowViewModel
            {
                SaveCommand = _commandFactory.UpdateUserCommand(),
                CancelCommand = _commandFactory.CloseWindowCommand(),
                User = user,
                Roles = _dataFactory.Roles,
                Languages = _languageService.Languages
            };
        }


        public object CreateConfirmDeleteViewModel(UserRead user)
        {
            return new ConfirmDeleteWindowViewModel
            {
                MessageDetail = string.Format("User: {0}", user.DisplayName),
                DeleteCommand = _commandFactory.DeleteUserCommand(user),
                CancelCommand = _commandFactory.CloseWindowCommand(),
                DeletingItem = user
            };
        }


        public object CreateUserAddViewModel(UserWrite user)
        {
            return new UserEditorWindowViewModel
            {
                SaveCommand = _commandFactory.CreateUserCommand(),
                CancelCommand = _commandFactory.CloseWindowCommand(),
                User = user,
                Roles = _dataFactory.Roles,
                Languages = _languageService.Languages
            };
        }


        internal TabItemViewModel CreatePermissionManagerTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitlePermissions,
                HeaderIcon = "Permission",
                PageTitle = Localized.Resources.PageTitlePermissions,
                Toolbar = new GenericListToolbar { DataContext = CreatePermissionManagerViewModel() }
            };
        }


        internal object CreatePermissionManagerViewModel()
        {
            if (!_viewModels.ContainsKey(PermissionsViewModelKey))
            {
                _viewModels.Add(PermissionsViewModelKey, new PermissionManagerViewModel
                {
                    AddCommand = _commandFactory.AddRoleWindowCommand(),
                    //DeleteCommand = _commandFactory.DeleteUserConfirmWindowCommand(),
                    //EditCommand = _commandFactory.EditUserWindowCommand(),
                    //ToggleUserEnabledStatusCommand = _commandFactory.ToggleUserCommand(),
                    Items = _dataFactory.Roles
                });
            }

            return _viewModels[PermissionsViewModelKey];
        }


        public object CreateNameEditorAddViewModel(dynamic namedEntity)
        {
            return new NameEditorWindowViewModel
            {
                NamedEntity = namedEntity,
                SaveCommand = _commandFactory.AddRoleCommand(),
                CancelCommand = new CloseWindowCommand()
            };
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

//HeaderLabel = Localized.Resources.TabTitleMaterials,
                //HeaderIcon = "Package",
                //PageTitle = Localized.Resources.PageTitleMaterials,
                ////Toolbar = new GenericListToolbar { DataContext = _viewModel }
