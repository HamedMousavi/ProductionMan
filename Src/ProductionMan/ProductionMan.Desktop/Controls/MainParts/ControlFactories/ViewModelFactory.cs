using System.Collections.Generic;
using System.Collections.ObjectModel;
using ProductionMan.Desktop.Commands;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{
    public class ViewModelFactory
    {

        private Dictionary<string, object> _viewModels;

        private readonly Membership _membershipService;
        private readonly CommandFactory _commandFactory;
        private readonly ILanguageService _languageService;
        private readonly WindowManager _windowManager;
        private MainWindowDataProvider _dataProvider;

        public ViewModelFactory(Membership membershipService, CommandFactory commandFactory,
            ILanguageService languageService, WindowManager windowManager, MainWindowDataProvider dataProvider)
        {
            _viewModels = new Dictionary<string, object>();

            _membershipService = membershipService;
            _commandFactory = commandFactory;
            _languageService = languageService;
            _windowManager = windowManager;
            _dataProvider = dataProvider;
        }


        internal object CreateUserManagerViewModel()
        {
            if (!_viewModels.ContainsKey("Users"))
            {
                _viewModels.Add("Users", new UserManagerViewModel
                {
                    AddCommand =
                        _commandFactory.CreateAddUserCommand(_windowManager, _membershipService, _languageService),
                    DeleteCommand = _commandFactory.CreateDeleteUserCommand(_windowManager, _membershipService),
                    EditCommand = _commandFactory.CreateEditUserCommand(_windowManager, _membershipService),
                    ToggleUserEnabledStatusCommand = _commandFactory.ToggleUserCommand(),
                    Items = _dataProvider.Get<ObservableCollection<UserRead>>("Users")
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
