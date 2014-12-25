using System.Collections.Generic;
using ProductionMan.Desktop.Repositories;
using ProductionMan.Web.Api.Common.Models;
using System.Threading.Tasks;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class MainWindowDataProvider
    {

        private readonly MembershipRepository _membershipRepository;
        private readonly InMemoryDataStore _database;


        public MainWindowDataProvider(MembershipRepository membershipRepository)
        {
            _database = new InMemoryDataStore();
            _membershipRepository = membershipRepository;
        }


        internal async Task LoadData()
        {
            var permissions = await LoadPermissions();
            foreach (var permission in permissions)
            {
                await LoadData(permission);
            }
        }


        private async Task<IEnumerable<Permission>> LoadPermissions()
        {
            var permissions = await _membershipRepository.LoadUserPermissions();

            _database.Set("UserPermissions", permissions);

            return permissions;
        }


        private async Task LoadData(Permission permission)
        {
            if (permission.ResourceName == "Users")
            {
                _database.Set("Users", await _membershipRepository.LoadUsers());
            }
        }


        internal T Get<T>(string storeName) where T : class
        {
            return _database.Get(storeName) as T;
        }
    }
}
