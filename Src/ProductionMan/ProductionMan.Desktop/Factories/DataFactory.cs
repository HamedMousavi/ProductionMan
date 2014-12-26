using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProductionMan.Desktop.Repositories;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Desktop.Factories
{

    public class DataFactory
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
                Users = await _membershipRepository.LoadUsers();
            }
        }


        public IEnumerable<Permission> Permissions { get; set; }


        public ObservableCollection<UserRead> Users { get; set; }


        public delegate void LoadCompletedEvent(object sender, EventArgs e);

        public event LoadCompletedEvent LoadCompleted;


        protected virtual void OnLoadCompleted()
        {
            var handler = LoadCompleted;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public List<UserRole> Roles { get; set; }
    }
}
