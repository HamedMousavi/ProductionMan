using System.Windows.Controls;
using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Factories;

namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    class AboutUsFactory : IControlFactory
    {

        private AboutPageViewModel _viewModel;


        public void CreateViewModels(ViewModelFactory viewModelFactory)
        {
            _viewModel = viewModelFactory.CreateAboutViewModel();
        }


        public UserControl CreateUserControl()
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
