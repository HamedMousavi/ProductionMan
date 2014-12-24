using System.Threading.Tasks;
using System.Windows.Controls;
using ProductionMan.Desktop.Commands;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    class AboutUsFactory : IControlFactory
    {

        private readonly CommandFactory _commandFactory;


        public AboutUsFactory(CommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }


#pragma warning disable 1998
        public async Task<UserControl> CreateUserControl()
#pragma warning restore 1998
        {
            return new AboutPage
            {
                DataContext = new AboutPageViewModel
                {
                    OpenUrlCommand = new NavigateToWebsiteCommand()
                }
            };
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitleAbout,
                HeaderIcon = "Info",
                PageTitle = Localized.Resources.PageTitleAbout
            };
        }
    }
}
