using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class ProcessManagerFactory : IControlFactory
    {

        
        private readonly Membership _membershipService;
        private ProcessManagerViewModel _viewModel;


        public ProcessManagerFactory(Membership membershipService, ProcessManagerViewModel viewModel)
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
                HeaderLabel = Localized.Resources.TabTitleProcesses,
                HeaderIcon = "Process",
                PageTitle = Localized.Resources.PageTitleProcesses,
                Toolbar = new GenericListToolbar { DataContext = _viewModel }
            };
        }
    }
}
