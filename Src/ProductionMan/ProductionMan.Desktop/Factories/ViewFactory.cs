using System.Windows;
using ProductionMan.Web.Api.Common.Models;

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


        public Window CreateUserEditView(UserWrite user)
        {
            return new UserEditorWindow
            {
                DataContext = _viewModelFactory.CreateUserEditViewModel(user)
            };
        }


        public Window CreateUserDeleteView(UserRead user)
        {
            return new ConfirmDeleteWindow
            {
                DataContext = _viewModelFactory.CreateConfirmDeleteViewModel(user)
            };
        }

        public Window CreateUserAddView(UserWrite user)
        {
            return new UserEditorWindow
            {
                DataContext = _viewModelFactory.CreateUserAddViewModel(user)
            };
        }
    }
}