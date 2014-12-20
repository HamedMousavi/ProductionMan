using log4net;
using log4net.Config;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;


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
        private ILog _logger;


        private void StartApplication(object sender, StartupEventArgs e)
        {
            // Configure logger
            SetupLogger();

            SetupGlobalExceptionHandlers();

            SetupLanguage();

            SetupSecurity();

            SetupCommandFactory();

            SetupWindowManager();

            StartApplicationWindow();
        }


        private void SetupLogger()
        {
            XmlConfigurator.Configure();
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _logger.Info("Application Started!");
        }


        private void SetupGlobalExceptionHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;
            Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
        }


        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Error("Unhandled global", e.Exception);
            e.Handled = true;
        }


        private void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Error(e.ToString());
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
            //var culture = new CultureInfo("fa-IR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement), new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }


        private void StartApplicationWindow()
        {
            _windowManager.DisplayLoginWindow(_user);
        }
    }
}
