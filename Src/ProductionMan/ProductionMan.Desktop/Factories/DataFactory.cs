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
            Permissions = await _membershipRepository.LoadUserPermissions();
            foreach (var permission in Permissions)
            {
                await LoadData(permission);
            }
            
            OnLoadCompleted();
        }


        private async Task LoadData(Permission permission)
        {
            if (permission.ResourceName == "Users")
            {
                Roles = await _membershipRepository.LoadRoles();
                Users = await _membershipRepository.LoadUsers();
            }
        }


        public ObservableCollection<Permission> Permissions { get; set; }

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
