using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class StoreManagerFactory : IControlFactory
    {
        
        private readonly Membership _membershipService;
        private StoreManagerViewModel _viewModel;


        public StoreManagerFactory(Membership membershipService, StoreManagerViewModel viewModel)
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
                HeaderLabel = Localized.Resources.TabTitleStores,
                HeaderIcon = "Stores",
                PageTitle = Localized.Resources.PageTitleStores,
                Toolbar = new GenericListToolbar { DataContext = _viewModel }
            };
        }
    }
}
