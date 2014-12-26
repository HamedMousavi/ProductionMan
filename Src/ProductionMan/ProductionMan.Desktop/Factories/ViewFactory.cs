using System.Windows;
using ProductionMan.Desktop.Controls;
using ProductionMan.Domain.Security;


namespace ProductionMan.Desktop.Factories
{

    public class ViewFactory
    {

        private readonly ViewModelFactory _viewModelFactory;


        public ViewFactory(ViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }


        public Window CreateLoginWindow()
        {
            return new LoginWindow {DataContext = _viewModelFactory.CreateLoginWindowViewModel()};
        }


        internal Window CreateMainView()
        {
            return new MainWindow
            {
                DataContext = _viewModelFactory.CreateMainWindowViewModel()
            };
        }
    }
}