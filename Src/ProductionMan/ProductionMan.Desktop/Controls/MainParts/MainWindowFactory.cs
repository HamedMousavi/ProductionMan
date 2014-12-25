using ProductionMan.Desktop.Commands;
using ProductionMan.Desktop.Controls.MainParts.ControlFactories;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.WebServices;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class MainWindowFactory
    {

        private readonly Membership _membershipService;
        private readonly CommandFactory _commandFactory;
        private readonly ILanguageService _languageService;
        private readonly WindowManager _windowManager;


        public MainWindowFactory(Membership membershipService, CommandFactory commandFactory, ILanguageService languageService, WindowManager windowManager)
        {
            _membershipService = membershipService;
            _commandFactory = commandFactory;
            _languageService = languageService;
            _windowManager = windowManager;
        }


        public UserManagerFactory CreateUserManagerFactory()
        {
            var viewModel = new UserManagerViewModel
            {
                AddCommand = _commandFactory.CreateAddUserCommand(_windowManager, _membershipService),
                DeleteCommand = _commandFactory.CreateDeleteUserCommand(_windowManager, _membershipService),
                EditCommand = _commandFactory.CreateEditUserCommand(_windowManager, _membershipService),
                ToggleUserEnabledStatusCommand = _commandFactory.ToggleUserCommand()
            };

            return new UserManagerFactory(_membershipService, viewModel);
        }


        internal IControlFactory CreatePermissionManagerFactory()
        {
            var viewModel = new PermissionManagerViewModel
            {
                AddCommand = _commandFactory.CreateAddUserCommand(_windowManager, _membershipService),
                DeleteCommand = _commandFactory.CreateDeleteUserCommand(_windowManager, _membershipService),
                EditCommand = _commandFactory.CreateEditUserCommand(_windowManager, _membershipService)
            };

            return new PermissionManagerFactory(_membershipService, viewModel);
        }


        internal IControlFactory CreateMaterialManagerFactory()
        {
            var viewModel = new MaterialManagerViewModel
            {
                AddCommand = _commandFactory.CreateAddUserCommand(_windowManager, _membershipService),
                DeleteCommand = _commandFactory.CreateDeleteUserCommand(_windowManager, _membershipService),
                EditCommand = _commandFactory.CreateEditUserCommand(_windowManager, _membershipService)
            };

            return new MaterialManagerFactory(_membershipService, viewModel);
        }


        internal IControlFactory CreateProcessManagerFactory()
        {
            var viewModel = new ProcessManagerViewModel
            {
                AddCommand = _commandFactory.CreateAddUserCommand(_windowManager, _membershipService),
                DeleteCommand = _commandFactory.CreateDeleteUserCommand(_windowManager, _membershipService),
                EditCommand = _commandFactory.CreateEditUserCommand(_windowManager, _membershipService)
            };

            return new ProcessManagerFactory(_membershipService, viewModel);
        }


        internal IControlFactory CreateStoreManagerFactory()
        {
            var viewModel = new StoreManagerViewModel
            {
                AddCommand = _commandFactory.CreateAddUserCommand(_windowManager, _membershipService),
                DeleteCommand = _commandFactory.CreateDeleteUserCommand(_windowManager, _membershipService),
                EditCommand = _commandFactory.CreateEditUserCommand(_windowManager, _membershipService)
            };

            return new StoreManagerFactory(_membershipService, viewModel);
        }


        internal IControlFactory CreateAboutUsFactory()
        {
            return new AboutUsFactory(new AboutPageViewModel {OpenUrlCommand = new NavigateToWebsiteCommand()});
        }


        internal IControlFactory CreateSettingsManagerFactory()
        {
            return new SettingsManagerFactory(new SettingsPageViewModel(_languageService));
        }
    }
}