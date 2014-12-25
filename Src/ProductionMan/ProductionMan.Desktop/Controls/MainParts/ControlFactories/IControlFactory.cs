using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{
    public interface IControlFactory
    {
        void CreateViewModels(ViewModelFactory viewModelFactory);

        UserControl CreateUserControl();
        
        TabItemViewModel CreateTabItemViewModel();
    }
}
