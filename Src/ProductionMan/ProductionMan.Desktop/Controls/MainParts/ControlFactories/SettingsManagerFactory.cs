using System.Threading.Tasks;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    class SettingsManagerFactory : IControlFactory
    {

        private readonly SettingsPageViewModel _viewModel;


        public SettingsManagerFactory(SettingsPageViewModel viewModel)
        {
            _viewModel = viewModel;
        }


        public async Task<UserControl> CreateUserControl()
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
