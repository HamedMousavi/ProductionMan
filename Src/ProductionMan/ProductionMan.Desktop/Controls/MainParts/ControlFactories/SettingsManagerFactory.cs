using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    class SettingsManagerFactory : IControlFactory
    {

        private SettingsPageViewModel _viewModel;


        public void CreateViewModels(ViewModelFactory viewModelFactory)
        {
            _viewModel = viewModelFactory.CreateSettingsViewModel();
        }


        public UserControl CreateUserControl()
        {
            return new SettingsPage {DataContext = _viewModel};
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitleSettings,
                HeaderIcon = "Settings",
                PageTitle = Localized.Resources.PageTitleSettings
            };
        }
    }
}
