using System.Globalization;
using System.Threading;
using System.Windows;


namespace ProductionMan.Desktop
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        private Domain.Security.User _user;
        private WindowManager _windowManager;
        private CommandFactory _commandFactory;


        private void StartApplication(object sender, StartupEventArgs e)
        {
            SetupLanguage();

            SetupSecurity();

            SetupCommandFactory();

            SetupWindowManager();

            StartApplicationWindow();
        }


        private void SetupSecurity()
        {
            _user = new Domain.Security.User();
        }


        private void SetupCommandFactory()
        {
            _commandFactory = new CommandFactory();
        }


        private void SetupWindowManager()
        {
            _windowManager = new WindowManager(_commandFactory);
        }


        private void SetupLanguage()
        {
            var culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }


        private void StartApplicationWindow()
        {
            _windowManager.DisplayLoginWindow(_user);
        }
    }
}
