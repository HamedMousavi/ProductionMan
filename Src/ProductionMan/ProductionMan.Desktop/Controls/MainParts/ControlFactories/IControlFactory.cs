using System.Windows.Controls;
using ProductionMan.Desktop.Factories;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{
    public interface IControlFactory
    {
        void CreateViewModels(ViewModelFactory viewModelFactory);

        UserControl CreateUserControl();
        
        TabItemViewModel CreateTabItemViewModel();
    }
}
