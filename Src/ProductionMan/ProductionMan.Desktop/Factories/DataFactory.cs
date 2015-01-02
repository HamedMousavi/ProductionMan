using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProductionMan.Desktop.Repositories;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Factories
{

    public sealed class DataFactory
    {

        private readonly MembershipRepository _membershipRepository;


        public DataFactory(MembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }


        internal async Task Load()
        {
            UserPermissions = await _membershipRepository.LoadUserPermissions();
            if (UserPermissions == null) return;
            
            // All users should be able to see these parts of the app
            UserPermissions.Add(new Permission { ResourceName = "/api/settings" });
            UserPermissions.Add(new Permission { ResourceName = "/api/about" });

            // Load data required for other parts of the app
            foreach (var permission in UserPermissions)
            {
                await LoadData(permission);
            }
            
            OnLoadCompleted();
        }


        private async Task LoadData(Permission permission)
        {
            if (permission.ResourceName == "/api/users")
            {
                Roles = await _membershipRepository.LoadRoles();
                Users = await _membershipRepository.LoadUsers();
            }
            else if (permission.ResourceName == "/api/permissions")
            {
                AllPermissions = await _membershipRepository.LoadAllPermissions();
            }
        }


        // All available permissions in the system
        public IEnumerable<Permission> AllPermissions { get; set; }
        
        // Permissions assigned to current user
        public IList<Permission> UserPermissions { get; private set; }

        public ObservableCollection<UserRead> Users { get; set; }

        public ObservableCollection<UserRole> Roles { get; set; }
        #region events

        public delegate void LoadCompletedEvent(object sender, EventArgs e);

        public event LoadCompletedEvent LoadCompleted;


        private void OnLoadCompleted()
        {
            var handler = LoadCompleted;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        
        #endregion events
    }
}
