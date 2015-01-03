using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Factories;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class RawMillManagerFactory : IControlFactory
    {

        private RawMillPageViewModel _viewModel;


        public void CreateViewModels(ViewModelFactory viewModelFactory)
        {
            _viewModel = viewModelFactory.CreateRawMillPageViewModel();
        }


        public UserControl CreateUserControl()
        {
            return new RawMillPage { DataContext = _viewModel };
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitleRawMill,
                HeaderIcon = "RawMill",
                PageTitle = Localized.Resources.PageTitleRawMill
            };
        }
    }
}
