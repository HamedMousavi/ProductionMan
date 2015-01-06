using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using AutoMapper;
using log4net;
using log4net.Config;
using ProductionMan.Common;
using ProductionMan.Desktop.Factories;
using ProductionMan.Desktop.Properties;
using ProductionMan.Desktop.Repositories;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.AppStatus;
using ProductionMan.Domain.Globalization;
using ProductionMan.Domain.Security;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Desktop
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        private ILog _logger;
        private AppServicesFactory _appServiceFactory;


        private void StartApplication(object sender, StartupEventArgs e)
        {
            SetupLogger();

            SetupGlobalExceptionHandlers();

            SetupAutoMapper();

            SetupAppServices();

            SetupLanguage();

            SetupDomain();

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


        private void SetupAutoMapper()
        {
            Mapper.CreateMap<UserRead, UserWrite>();
        }


        private void SetupAppServices()
        {
            // Setup GUI helpers
            SharedApplicationServices.Instanse.SynchronizationContext =
                SynchronizationContext.Current;

            _appServiceFactory = new AppServicesFactory();
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

            _appServiceFactory.CreateLanguageService(languages).
                LoadDefaultLanguage(languages[1], languages[0]);
        }


        private void SetupDomain()
        {
            Domain.Settings.ServerBaseAddress = Settings.Default.ServerBaseAddress;
        }


        private void StartApplicationWindow()
        {
            var windowManager = new WindowManager();
            var membershipService = new Membership();
            var membershipRepository = new MembershipRepository(membershipService);
            _appServiceFactory.CreateStatusService().Register(membershipRepository);
            var commandFactory = new CommandFactory(windowManager, membershipRepository);
            var dataFactory = CreateDataFactory(windowManager, membershipRepository);
            var user = CreateUser(membershipService, dataFactory);

            var viewModelFactory = new ViewModelFactory(commandFactory,
                _appServiceFactory.CreateLanguageService(), user, _appServiceFactory, dataFactory);
            
            windowManager.Setup(viewModelFactory, _appServiceFactory.CreateFontService());
            windowManager.DisplayLoginWindow();

            GreetUser();
        }


        private DataFactory CreateDataFactory(WindowManager windowManager, MembershipRepository membershipRepository)
        {
            var dataFactory = new DataFactory(membershipRepository);
            dataFactory.LoadCompleted += (sender, args) => windowManager.DisplayMainWindow();

            return dataFactory;
        }


        private User CreateUser(Membership membershipService, DataFactory dataFactory)
        {
            var user = new User(membershipService);

            user.PropertyChanged += async (sender, e) =>
            {
                if (e.NameIs("LoginStatus"))
                {
                    if (user.LoginStatus == User.LoginStates.SignedIn)
                    {
                        // Load data asyncronously
                        await dataFactory.Load();
                    }
                }
            };

            return user;
        }


        private void GreetUser()
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

            _appServiceFactory.CreateStatusService().SetStatus(Status.Levels.Info, message);
        }

    }
}
