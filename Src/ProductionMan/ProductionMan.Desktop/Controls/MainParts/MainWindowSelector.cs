using ProductionMan.Desktop.Controls.MainParts.ControlFactories;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class MainWindowSelector : BaseContentSelector<TabItemViewModel>
    {

        private ObservableCollection<TabItemViewModel> _tabs;
        private readonly MainWindowDataProvider _dataProvider;
        private readonly MainWindowFactory _windowFactory;
        private Dictionary<string, IControlFactory> _controlFactories;
        private readonly ViewModelFactory _viewModelFactory;


        public MainWindowSelector(MainWindowDataProvider dataProvider, MainWindowFactory windowFactory, ViewModelFactory viewModelFactory)
        {
            _dataProvider = dataProvider;
            _windowFactory = windowFactory;
            _viewModelFactory = viewModelFactory;

            CreateControlFactories();
        }


        private void CreateControlFactories()
        {
            _controlFactories = new Dictionary<string, IControlFactory>
            {
                {"Users",       _windowFactory.CreateUsersFactory()             },
                {"Permissions", _windowFactory.CreateFPermissionsactory()       },
                {"Materials",   _windowFactory.CreateMaterialsFactory()         },
                {"Processes",   _windowFactory.CreateProcessesFactory()         },
                {"Stores",      _windowFactory.CreateStoresFactory()            },
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


        internal void CreateContent()
        {
            var permissions = _dataProvider.Get<IEnumerable<Permission>>("UserPermissions");
            foreach (var permission in permissions)
            {
                CreateContent(permission);
            }
        }


        private void CreateContent(Permission permission)
        {
            if (_controlFactories.ContainsKey(permission.ResourceName))
            {
                var factory = _controlFactories[permission.ResourceName];
                if (factory != null)
                {
                    factory.CreateViewModels(_viewModelFactory);
                    AddContent(factory.CreateTabItemViewModel(), factory.CreateUserControl());
                }
            }
        }
    }
}
