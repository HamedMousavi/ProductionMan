using ProductionMan.Desktop.Services;
using ProductionMan.Domain.WebServices;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    class SettingsManagerFactory : IControlFactory
    {
        private readonly ILanguageService _languageService;
        private readonly CommandFactory _commandFactory;


        public SettingsManagerFactory(ILanguageService languageService, CommandFactory commandFactory)
        {
            _languageService = languageService;
            _commandFactory = commandFactory;
        }


        public async Task<UserControl> CreateUserControl()
        {
            return new SettingsPage()
            {
                DataContext = new SettingsPageViewModel(_languageService)
            };
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitleSettings,
                HeaderIcon = "Settings",
                PageTitle = Localized.Resources.PageTitleSettings
            };
        }
    }
}
