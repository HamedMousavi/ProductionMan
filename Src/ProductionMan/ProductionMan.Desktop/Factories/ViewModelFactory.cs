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
        //private const string PermissionsViewModelKey = "Permissions";
        private const string RolesViewModelKey = "Roles";


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
            contentSelector.CreateContent(_dataFactory.UserPermissions);
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


        public object CreateUserConfirmDeleteViewModel(UserRead user)
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


        internal TabItemViewModel CreateRoleManagerTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitlePermissions,
                HeaderIcon = "Permission",
                PageTitle = Localized.Resources.PageTitlePermissions,
                Toolbar = new GenericListToolbar { DataContext = CreateRoleManagerViewModel() }
            };
        }


        internal object CreateRoleManagerViewModel()
        {
            if (!_viewModels.ContainsKey(RolesViewModelKey))
            {
                var viewModel = new RoleManagerPageViewModel
                {
                    Roles = _dataFactory.Roles,
                    AddCommand = _commandFactory.AddRoleWindowCommand(),
                    DeleteCommand = _commandFactory.DeleteRoleConfirmWindowCommand(),
                    EditCommand = _commandFactory.EditRoleWindowCommand(),
                    ToggleRolePermissionAssignment = _commandFactory.RolePermissionAssignmentCommand()
                };

                viewModel.SetPermissions(_dataFactory.AllPermissions);

                _viewModels.Add(RolesViewModelKey, viewModel);
            }

            return _viewModels[RolesViewModelKey];
        }

        public object CreateRoleAddViewModel(UserRole role)
        {
            return new RoleEditorWindowViewModel
            {
                Role = role,
                SaveCommand = _commandFactory.AddRoleCommand(),
                CancelCommand = _commandFactory.CloseWindowCommand(),
            };
        }

        internal object CreateRoleConfirmDeleteViewModel(UserRole role)
        {
            return new ConfirmDeleteWindowViewModel
            {
                MessageDetail = string.Format("Role: {0}", role.RoleName),
                DeleteCommand = _commandFactory.DeleteRoleCommand(role),
                CancelCommand = _commandFactory.CloseWindowCommand(),
                DeletingItem = role
            };
        }

        internal object CreateRoleEditViewModel(UserRole role)
        {
            return new RoleEditorWindowViewModel
            {
                Role = role,
                SaveCommand = _commandFactory.EditRoleCommand(),
                CancelCommand = _commandFactory.CloseWindowCommand(),
            };
        }

        public CrusherPageViewModel CreateCrusherWindowViewModel()
        {
            return new CrusherPageViewModel();
        }
    }
}
