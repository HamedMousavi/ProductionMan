using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Factories;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class RawMaterialSiloFactory : IControlFactory
    {

        private RawMaterialSiloPageViewModel _viewModel;


        public void CreateViewModels(ViewModelFactory viewModelFactory)
        {
            _viewModel = viewModelFactory.CreateRawMaterialSiloPageViewModel();
        }


        public UserControl CreateUserControl()
        {
            return new RawMaterialSiloPage { DataContext = _viewModel };
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitleRawMaterial,
                HeaderIcon = "RawMaterial",
                PageTitle = Localized.Resources.PageTitleRawMaterial
            };
        }
    }
}
