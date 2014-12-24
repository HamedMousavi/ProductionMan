using System.Threading.Tasks;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    class AboutUsFactory : IControlFactory
    {

        private readonly AboutPageViewModel _viewModel;


        public AboutUsFactory(AboutPageViewModel viewModel)
        {
            _viewModel = viewModel;
        }


#pragma warning disable 1998
        public async Task<UserControl> CreateUserControl()
#pragma warning restore 1998
        {
            return new AboutPage {DataContext = _viewModel};
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitleAbout,
                HeaderIcon = "Info",
                PageTitle = Localized.Resources.PageTitleAbout
            };
        }
    }
}
