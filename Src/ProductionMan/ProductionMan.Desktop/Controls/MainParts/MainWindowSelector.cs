using ProductionMan.Desktop.Controls.MainParts.ControlFactories;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class MainWindowSelector : BaseContentSelector<TabItemViewModel>
    {

        private ObservableCollection<TabItemViewModel> _tabs;
        private readonly Membership _membershipService;
        private readonly MainWindowFactory _windowFactory;
        private Dictionary<string, IControlFactory> _controlFactories;


        public MainWindowSelector(Membership membershipService, MainWindowFactory windowFactory)
        {
            _membershipService = membershipService;
            _windowFactory = windowFactory;

            CreateControlFactories();
        }


        private void CreateControlFactories()
        {
            _controlFactories = new Dictionary<string, IControlFactory>
            {
                {"Users",       _windowFactory.CreateUserManagerFactory()       },
                {"Permissions", _windowFactory.CreatePermissionManagerFactory() },
                {"Materials",   _windowFactory.CreateMaterialManagerFactory()   },
                {"Processes",   _windowFactory.CreateProcessManagerFactory()    },
                {"Stores",      _windowFactory.CreateStoreManagerFactory()      },
                {"Settings",    _windowFactory.CreateSettingsManagerFactory()   },
                {"About",       _windowFactory.CreateAboutUsFactory()           },
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
                await AddContent(permission);
            }

            return permissions;
        }


        private async Task AddContent(Permission permission)
        {
            if (_controlFactories.ContainsKey(permission.ResourceName))
            {
                var factory = _controlFactories[permission.ResourceName];
                AddContent(factory.CreateTabItemViewModel(), await factory.CreateUserControl());
            }
        }
    }
}
