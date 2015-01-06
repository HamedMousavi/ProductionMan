using ProductionMan.Web.Api.Common.Models;
using System.Windows;
using System.Windows.Media;


namespace ProductionMan.Desktop.Factories
{

    public class ViewFactory
    {

        private readonly ViewModelFactory _viewModelFactory;
        private readonly FontFamily _fontFamily;


        public ViewFactory(ViewModelFactory viewModelFactory, FontFamily fontFamily)
        {
            _viewModelFactory = viewModelFactory;
            _fontFamily = fontFamily;
        }


        public Window CreateLoginWindow()
        {
            return new LoginWindow {DataContext = _viewModelFactory.CreateLoginWindowViewModel()};
        }


        internal Window CreateMainView()
        {
            return new MainWindow
            {
                DataContext = _viewModelFactory.CreateMainWindowViewModel(),
                FontFamily = _fontFamily
            };
        }


        public Window CreateUserEditView(UserWrite user)
        {
            return new UserEditorWindow
            {
                DataContext = _viewModelFactory.CreateUserEditViewModel(user),
                FontFamily = _fontFamily
            };
        }


        public Window CreateUserDeleteView(UserRead user)
        {
            return new ConfirmDeleteWindow
            {
                DataContext = _viewModelFactory.CreateUserConfirmDeleteViewModel(user),
                FontFamily = _fontFamily
            };
        }

        public Window CreateUserAddView(UserWrite user)
        {
            return new UserEditorWindow
            {
                DataContext = _viewModelFactory.CreateUserAddViewModel(user),
                FontFamily = _fontFamily
            };
        }

        public Window CreateRoleAddView(UserRole role)
        {
            return new RoleEditorWindow
            {
                DataContext = _viewModelFactory.CreateRoleAddViewModel(role),
                FontFamily = _fontFamily
            };
        }


        internal Window CreateRoleDeleteView(UserRole role)
        {
            return new ConfirmDeleteWindow
            {
                DataContext = _viewModelFactory.CreateRoleConfirmDeleteViewModel(role),
                FontFamily = _fontFamily
            };
        }

        public Window CreateRoleEditView(UserRole role)
        {
            return new RoleEditorWindow
            {
                DataContext = _viewModelFactory.CreateRoleEditViewModel(role),
                FontFamily = _fontFamily
            };
        }
    }
}