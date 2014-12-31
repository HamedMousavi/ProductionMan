using AutoMapper;
using ProductionMan.Domain.AppStatus;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace ProductionMan.Desktop.Repositories
{

    public class MembershipRepository : StatusObservableBase
    {

        private readonly Membership _membershipService;
        private ObservableCollection<Permission> _permissions;
        private ObservableCollection<UserRead> _users;
        private ObservableCollection<UserRole> _roles;


        public MembershipRepository(Membership membershipService)
        {
            _membershipService = membershipService;
        }


        internal async Task<ObservableCollection<Permission>> LoadUserPermissions()
        {
            if (_permissions == null)
            {
                var permissions = await _membershipService.GetPermissions();
                if (permissions != null && permissions.Results != null)
                {
                    _permissions = new ObservableCollection<Permission>();
                    foreach (var permission in permissions.Results)
                    {
                        _permissions.Add(permission);
                    }
                }
            }

            return _permissions;
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


        internal async Task UpdateUser(UserWrite userWrite)
        {
            LogWarning(Localized.Resources.StatusConnecting);

            var successful = await _membershipService.UpdateUser(userWrite);
            if (successful)
            {
                var index = IndexOfUserById(userWrite);
                _users.RemoveAt(index);
                _users.Insert(index, Mapper.Map<UserRead>(userWrite));

                LogSuccess(Localized.Resources.StatusItemUpdated);
            }
            else
            {
                LogFailure(Localized.Resources.StatusFailedToUpdatedItem);
            }
        }


        private int IndexOfUserById(UserWrite userWrite)
        {
            for (var i = 0; i < _users.Count; i++)
            {
                var user = _users[i];
                if (user.UserId == userWrite.UserId) return i;
            }

            return -1;
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

        internal Task CreateRole(UserRole userRole)
        {
            throw new System.NotImplementedException();
        }
    }
}
