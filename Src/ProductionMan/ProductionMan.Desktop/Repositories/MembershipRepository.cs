using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Repositories
{

    public class MembershipRepository
    {

        private readonly Membership _membershipService;
        private List<Permission> _permissions;
        private ObservableCollection<UserRead> _users;


        public MembershipRepository(Membership membershipService)
        {
            _membershipService = membershipService;
        }


        internal async Task<IEnumerable<Permission>> LoadUserPermissions()
        {
            return _permissions ?? (_permissions = (await _membershipService.GetPermissions()).Results);
        }


        internal async Task<ObservableCollection<UserRead>> LoadUsers()
        {
            if (_users == null)
            {
                var userReads = (await _membershipService.GetUsers()).Results;

                _users = new ObservableCollection<UserRead>();
                foreach (var userRead in userReads)
                {
                    _users.Add(userRead);
                }
            }

            return _users;
        }
    }
}
