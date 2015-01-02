using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Factories;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class CrusherWindowManager : IControlFactory
    {

        private CrusherPageViewModel _viewModel;


        public void CreateViewModels(ViewModelFactory viewModelFactory)
        {
            _viewModel = viewModelFactory.CreateCrusherWindowViewModel();
        }


        public UserControl CreateUserControl()
        {
            return new CrusherPage { DataContext = _viewModel };
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitleCrusher,
                HeaderIcon = "Crusher",
                PageTitle = Localized.Resources.PageTitleCrusher
            };
        }
    }
}
