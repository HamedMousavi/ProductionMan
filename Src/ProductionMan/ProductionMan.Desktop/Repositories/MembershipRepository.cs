using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProductionMan.Domain.AppStatus;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Desktop.Repositories
{

    public class MembershipRepository : StatusObservableBase
    {

        private readonly Membership _membershipService;
        private List<Permission> _permissions;
        private ObservableCollection<UserRead> _users;
        private ObservableCollection<UserRole> _roles;


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

        internal async Task<ObservableCollection<UserRole>> LoadRoles()
        {
            if (_roles == null)
            {
                var roles = (await _membershipService.GetRoles()).Results;

                _roles = new ObservableCollection<UserRole>();
                foreach (var role in roles)
                {
                    _roles.Add(role);
                }
            }

            return _roles;
        }


        internal async Task CreateUser(UserWrite userWrite)
        {
            LogWarning(Localized.Resources.StatusConnecting);

            var successful = await _membershipService.CreateUser(userWrite);
            if (successful)
            {
                _users.Add(userWrite);
                LogSuccess(Localized.Resources.StatusItemCreated);
            }
            else
            {
                LogFailure(Localized.Resources.StatusFailedToCreateItem);
            }
        }


        internal async Task<bool> DeleteUser(UserRead userRead)
        {
            LogWarning(Localized.Resources.StatusConnecting);

            var successful = await _membershipService.DeleteUser(userRead);
            if (successful)
            {
                _users.Remove(userRead);
                LogSuccess(Localized.Resources.StatusItemDeleted);
                return true;
            }

            LogFailure(Localized.Resources.StatusFailedToDeleteItem);
            return false;
        }


        internal void UpdateUser(UserWrite userWrite)
        {
            throw new System.NotImplementedException();
        }


        #region StatusEvents

        private void LogFailure(string message)
        {
            FireStatusChanged(Status.Levels.Failure, message);
        }

        private void LogSuccess(string message)
        {
            FireStatusChanged(Status.Levels.Success, message);
        }

        private void LogWarning(string message)
        {
            FireStatusChanged(Status.Levels.Warning, message);
        }
        #endregion StatusEvents
    }
}
