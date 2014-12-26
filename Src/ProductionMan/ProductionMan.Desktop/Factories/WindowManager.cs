using System.Windows;
using ProductionMan.Desktop.Commands;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Factories
{

    public class WindowManager : IUserWindowManager
    {

        //private readonly AppServicesFactory _appServiceFactory;
        private ViewFactory _viewFactory;
        private Window _loginWindow;


        public void Setup(ViewModelFactory viewModelFactory)
        {
            // Create ViewFactory
            _viewFactory = new ViewFactory(viewModelFactory);
        }



        public void DisplayLoginWindow()
        {
            if (_loginWindow == null) _loginWindow = _viewFactory.CreateLoginWindow();
            _loginWindow.Show();
        }


        internal void DisplayMainWindow()
        {
            _viewFactory.CreateMainView().Show();

            if (_loginWindow != null && _loginWindow.IsVisible) _loginWindow.Close();
        }


        public void DisplayUserEditWindow(UserWrite user)
        {
            _viewFactory.CreateUserEditView(user).Show();
        }


        public void RequestPermissionToDelete(UserRead user)
        {
            _viewFactory.CreateUserDeleteView(user).Show();
        }


        public void DisplayUserAddWindow(UserWrite user)
        {
            _viewFactory.CreateUserAddView(user).Show();
        }

    }
}
