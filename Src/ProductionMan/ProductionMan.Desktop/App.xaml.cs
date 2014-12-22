using log4net;
using log4net.Config;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using ProductionMan.Domain.WebServices;


namespace ProductionMan.Desktop
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        private ILog _logger;


        private void StartApplication(object sender, StartupEventArgs e)
        {
            // Configure logger
            SetupLogger();

            SetupGlobalExceptionHandlers();

            SetupLanguage();

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


        private void SetupLanguage()
        {
            //var culture = new CultureInfo("en-US");
            var culture = new CultureInfo("fa-IR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement), new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }


        private void StartApplicationWindow()
        {
            var membershipService = new Membership();

            new WindowManager(new CommandFactory(), membershipService).
                DisplayLoginWindow(new Domain.Security.User(membershipService));
        }
    }
}
