using ProductionMan.Desktop.Commands;
using ProductionMan.Domain.Security;
using ProductionMan.Domain.WebServices;
using System.Windows;


namespace ProductionMan.Desktop.Factories
{

    public class WindowManager : IUserWindowManager
    {

        //private readonly AppServicesFactory _appServiceFactory;
        private readonly ViewFactory _viewFactory;
        private Window _loginWindow;


        public WindowManager(AppServicesFactory appServiceFactory, CommandFactory commandFactory, DataFactory dataFactory, Membership membershipService, User user)
        {
            // Create ViewFactory
            _viewFactory =
                new ViewFactory(
                    new ViewModelFactory(
                        membershipService,
                        commandFactory,
                        appServiceFactory.CreateLanguageService(),
                        null, user, appServiceFactory, dataFactory));

            // Register for login completion event in order to show main window
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


        public void DisplayUserEditorWindow(UserEditorWindowViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }


        public void RequestPermissionToDelete(ConfirmDeleteWindowViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }


        public void DisplayUserAddWindow(UserEditorWindowViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

    }
}
