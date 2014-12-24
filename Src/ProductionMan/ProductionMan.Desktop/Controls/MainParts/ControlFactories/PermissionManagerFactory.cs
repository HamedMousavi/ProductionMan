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
        private readonly PermissionManagerViewModel _viewModel;


        public PermissionManagerFactory(Membership membershipService, PermissionManagerViewModel viewModel)
        {
            _membershipService = membershipService;
            _viewModel = viewModel;
        }


        public async Task<UserControl> CreateUserControl()
        {
            //var response = await _membershipService.GetUsers();
            //var users = new ObservableCollection<User>();
            //if (response != null)
            //{
            //    foreach (var user in response.Results)
            //    {
            //        users.Add(user);
            //    }
            //}

            //_viewModel.Items = users;
            return new GenericListManager
            {
                DataContext = _viewModel
            };
        }

        public TabItemViewModel CreateTabItemViewModel()
        {
            return new TabItemViewModel
            {
                HeaderLabel = Localized.Resources.TabTitlePermissions,
                HeaderIcon = "Permission",
                PageTitle = Localized.Resources.PageTitlePermissions,
                Toolbar = new GenericListToolbar { DataContext = _viewModel }
            };
        }
    }
}
