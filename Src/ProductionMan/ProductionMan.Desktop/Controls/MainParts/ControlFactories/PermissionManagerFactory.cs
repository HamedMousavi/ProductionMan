using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    class PermissionManagerFactory : IControlFactory
    {

        private readonly Membership _membershipService;
        private readonly CommandFactory _commandFactory;


        public PermissionManagerFactory(Membership membership, CommandFactory commandFactory)
        {
            _membershipService = membership;
            _commandFactory = commandFactory;
        }


        public async Task<UserControl> CreateUserControl()
        {
            var response = await _membershipService.GetUsers();
            var users = new ObservableCollection<User>();
            if (response != null)
            {
                foreach (var user in response.Results)
                {
                    users.Add(user);
                }
            }

            return new GenericListManager
            {
                DataContext = new UserManagerViewModel
                {
                    Items = users,
                    AddCommand = _commandFactory.CreateAddUserCommand(),
                    DeleteCommand = _commandFactory.CreateDeleteUserCommand(),
                    ToggleUserEnabledStatusCommand = _commandFactory.ToggleUserCommand()
                }
            };
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitlePermissions,
                HeaderIcon = "Permission",
                PageTitle = Localized.Resources.PageTitlePermissions
            };
        }
    }
}
