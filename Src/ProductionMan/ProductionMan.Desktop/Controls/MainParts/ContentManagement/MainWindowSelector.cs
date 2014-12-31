﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using ProductionMan.Desktop.Controls.MainParts.ControlFactories;
using ProductionMan.Desktop.Factories;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Controls.MainParts.ContentManagement
{

    public class MainWindowSelector : BaseContentSelector<TabItemViewModel>
    {

        private ObservableCollection<TabItemViewModel> _tabs;
        private Dictionary<string, IControlFactory> _controlFactories;
        private Dictionary<string, IControlFactory> _createdContent;
        private readonly ViewModelFactory _viewModelFactory;


        public MainWindowSelector(ViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;

            CreateControlFactories();
        }


        private void CreateControlFactories()
        {
            // No content created yet
            _createdContent = new Dictionary<string, IControlFactory>();

            // All possible content
            _controlFactories = new Dictionary<string, IControlFactory>
            {
                {"/api/users", new UserManagerFactory()},
                {"/api/permissions", new PermissionManagerFactory()},
                {"/api/settings", new SettingsManagerFactory()},
                {"/api/about", new AboutUsFactory()},
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


        internal void CreateContent(IEnumerable<Permission> permissions)
        {
            foreach (var permission in permissions)
            {
                CreateContent(permission);
            }
        }


        private void CreateContent(Permission permission)
        {
            if (_controlFactories.ContainsKey(permission.ResourceName) &&
                !_createdContent.ContainsKey(permission.ResourceName))
            {
                var factory = _controlFactories[permission.ResourceName];
                if (factory != null)
                {
                    factory.CreateViewModels(_viewModelFactory);
                    AddContent(factory.CreateTabItemViewModel(), factory.CreateUserControl());
                    _createdContent.Add(permission.ResourceName, null);
                }
            }
        }
    }
}
