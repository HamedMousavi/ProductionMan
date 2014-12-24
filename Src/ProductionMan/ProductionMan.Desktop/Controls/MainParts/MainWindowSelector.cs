using System.Threading.Tasks;
using ProductionMan.Desktop.Controls.MainParts.ControlFactories;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class MainWindowSelector : BaseContentSelector<TabItemViewModel>
    {

        private ObservableCollection<TabItemViewModel> _tabs;
        private readonly Membership _membershipService;
        private Dictionary<string, IControlFactory> _controlFactories;
        private readonly CommandFactory _commandFactory;


        public MainWindowSelector(Membership membershipService, CommandFactory commandFactory)
        {
            _membershipService = membershipService;
            _commandFactory = commandFactory;

            CreateControlFactories();
        }


        private void CreateControlFactories()
        {
            _controlFactories = new Dictionary<string, IControlFactory>
            {
                {"Users", new UserManagerFactory(_membershipService, _commandFactory)},
                {"Permissions", new PermissionManagerFactory(_membershipService, _commandFactory)},
                {"Materials", new MaterialManagerFactory(_membershipService, _commandFactory)},
                {"Processes", new ProcessManagerFactory(_membershipService, _commandFactory)},
                {"Stores", new StoreManagerFactory(_membershipService, _commandFactory)},
                {"Settings", new SettingsManagerFactory(_membershipService, _commandFactory)},
                {"About", new AboutUsFactory(_commandFactory)}
            };
        }


        public ObservableCollection<TabItemViewModel> Tabs
        {
            get
            {
                if ((_tabs == null && Registry != null && Registry.Count > 0) ||
                    _tabs != null && Registry != null && _tabs.Count != Registry.Count)
                {
                    if (_tabs == null) _tabs = new ObservableCollection<TabItemViewModel>();
                    else _tabs.Clear();
                
                    foreach (var tab in Registry.Keys)
                    {
                        _tabs.Add(tab);
                    }
                }

                return _tabs;
            }
        }


        internal async Task<ServiceCallResult<List<Permission>>> CreateContent()
        {
            var permissions = await _membershipService.GetPermissions();

            foreach (var permission in permissions.Results)
            {
                AddContent(permission);
            }

            return permissions;
        }


        private async void AddContent(Permission permission)
        {
            if (_controlFactories.ContainsKey(permission.ResourceName))
            {
                var factory = _controlFactories[permission.ResourceName];
                AddContent(factory.CreateTabItemViewModel(), await factory.CreateUserControl());
            }
        }
    }
}
