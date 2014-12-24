using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    class MaterialManagerFactory : IControlFactory
    {

        private readonly Membership _membershipService;
        private readonly MaterialManagerViewModel _viewModel;


        public MaterialManagerFactory(Membership membershipService, MaterialManagerViewModel viewModel)
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
                HeaderLabel = Localized.Resources.TabTitleMaterials,
                HeaderIcon = "Package",
                PageTitle = Localized.Resources.PageTitleMaterials,
                Toolbar = new GenericListToolbar { DataContext = _viewModel }
            };
        }
    }
}
