using log4net;
using log4net.Config;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.Globalization;
using ProductionMan.Domain.WebServices;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;


namespace ProductionMan.Desktop
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        private ILog _logger;
        private ILanguageService _languageService;


        private void StartApplication(object sender, StartupEventArgs e)
        {
            // Configure logger
            SetupLogger();

            SetupGlobalExceptionHandlers();

            SharedApplicationServices.Instanse.SynchronizationContext = SynchronizationContext.Current;

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


        #region GlobalExceptions
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Error("Unhandled global", e.Exception);
            e.Handled = true;
        }


        private void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Error(e.ToString());
        }
        #endregion GlobalExceptions


        private void SetupLanguage()
        {
            var languages = new List<Language>
            {
                new Language {Id = 0, LocaleName = "en-US", Name = "English"},
                new Language {Id = 1, LocaleName = "fa-IR", Name = "فارسی"},
            };

            _languageService = new DefaultLanguageService {Languages = languages};
            _languageService.LoadDefaultLanguage(languages[1], languages[0]);
        }


        private void StartApplicationWindow()
        {
            var membershipService = new Membership();
            var statusService = new DefaultStatusService(_languageService);
            var commandFactory = new CommandFactory();

            new WindowManager(commandFactory, membershipService, _languageService, statusService).
                    DisplayLoginWindow(new Domain.Security.User(membershipService));

            SetStatus(statusService);
        }


        private void SetStatus(IStatusService statusService)
        {
            var message = string.Empty;
            var now = DateTime.Now.Hour;

            if (now >= 5 && now < 10) message = Localized.Resources.GoodMorning;
            else if (now >= 10 && now < 12) message = Localized.Resources.GoodDay;
            else if (now >= 12 && now < 13) message = Localized.Resources.GoodMidDay;
            else if (now >= 13 && now < 18) message = Localized.Resources.GoodAfternoon;
            else if (now >= 18 && now < 20) message = Localized.Resources.GoodEvening;
            else if (now >= 20 && now < 24) message = Localized.Resources.GoodNight;
            else if (now >= 24 || now < 5) message = Localized.Resources.GoodMorning;

            statusService.SetStatus(Status.Levels.Info, message);
        }
    }
}
