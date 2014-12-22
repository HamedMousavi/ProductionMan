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
                {"Users", new UserManagerFactory(_membershipService, _commandFactory)}
            };
        }

        //private Dictionary<string, TabItemViewModel> CreateViewModels()
        //{
        //    var vireModelProtoTypes = new Dictionary<string, TabItemViewModel>();
        //    vireModelProtoTypes.Add("Users", new TabItemViewModel { HeaderLabel = Localized.Resources.TabTitleUsers, HeaderIcon = "User", PageTitle = Localized.Resources.PageTitleUsers });
        //    vireModelProtoTypes.Add("Permissions", new TabItemViewModel { HeaderLabel = Localized.Resources.TabTitlePermissions, HeaderIcon = "User", PageTitle = Localized.Resources.PageTitlePermissions });
        //    vireModelProtoTypes.Add("Materials", new TabItemViewModel { HeaderLabel = Localized.Resources.TabTitleMaterials, HeaderIcon = "Package", PageTitle = Localized.Resources.PageTitleMaterials });
        //    vireModelProtoTypes.Add("Processes", new TabItemViewModel { HeaderLabel = Localized.Resources.TabTitleProcesses, HeaderIcon = "Process", PageTitle = Localized.Resources.PageTitleProcesses });
        //    vireModelProtoTypes.Add("Stores", new TabItemViewModel { HeaderLabel = Localized.Resources.TabTitleStores, HeaderIcon = "Stores", PageTitle = Localized.Resources.PageTitleStores });
        //    vireModelProtoTypes.Add("Settings", new TabItemViewModel { HeaderLabel = Localized.Resources.TabTitleSettings, HeaderIcon = "Settings", PageTitle = Localized.Resources.PageTitleSettings });
        //    vireModelProtoTypes.Add("About", new TabItemViewModel { HeaderLabel = Localized.Resources.TabTitleAbout, HeaderIcon = "User", PageTitle = Localized.Resources.PageTitleAbout });
        //    return vireModelProtoTypes;
        //}


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
