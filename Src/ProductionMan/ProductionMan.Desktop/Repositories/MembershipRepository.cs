using AutoMapper;
using ProductionMan.Domain.AppStatus;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


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


        internal async Task<IList<Permission>> LoadUserPermissions()
        {
            var permissions = await _membershipService.GetUserPermissions();
            return permissions != null ? permissions.Results : null;
        }


        public async Task<IEnumerable<Permission>> LoadAllPermissions()
        {
            var permissions = await _membershipService.GetAllPermissions();
            return permissions != null ? permissions.Results : null;
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


        internal async Task CreateRole(UserRole role)
        {
            LogWarning(Localized.Resources.StatusConnecting);

            var successful = await _membershipService.CreateRole(role);
            if (successful)
            {
                _roles.Add(role);
                LogSuccess(Localized.Resources.StatusItemCreated);
            }
            else
            {
                LogFailure(Localized.Resources.StatusFailedToCreateItem);
            }
        }


        internal async Task<bool> DeleteRole(UserRole role)
        {
            LogWarning(Localized.Resources.StatusConnecting);

            var successful = await _membershipService.DeleteRole(role);
            if (successful)
            {
                _roles.Remove(role);
                LogSuccess(Localized.Resources.StatusItemDeleted);
                return true;
            }

            LogFailure(Localized.Resources.StatusFailedToDeleteItem);
            return false;
        }


        internal async Task UpdateRole(UserRole role)
        {
            LogWarning(Localized.Resources.StatusConnecting);

            var successful = await _membershipService.Update(role);
            if (successful)
            {
                var index = _roles.IndexOf(role);
                _roles.RemoveAt(index);
                _roles.Insert(index, role);

                LogSuccess(Localized.Resources.StatusItemUpdated);
            }
            else
            {
                LogFailure(Localized.Resources.StatusFailedToUpdatedItem);
            }
        }


        internal async Task<bool> AssignPermissionToRole(UserRole role, Permission permission)
        {
            LogWarning(Localized.Resources.StatusConnecting);

            var successful = await _membershipService.RolePermissionAssign(role, permission);
            if (successful)
            {
                if (role.Permissions == null)
                {
                    role.Permissions = new List<Permission> { permission };
                }
                else
                {
                    role.Permissions = new List<Permission>(role.Permissions) { permission };
                }

                LogSuccess(Localized.Resources.StatusItemUpdated);
                return true;
            }

            LogFailure(Localized.Resources.StatusFailedToUpdatedItem);
            return false;
        }

        internal async Task<bool> RetractPermissionFromRole(UserRole role, Permission permission)
        {
            LogWarning(Localized.Resources.StatusConnecting);

            var successful = await _membershipService.RolePermissionRevoke(role, permission);
            if (successful)
            {
                role.Permissions = role.Permissions.Where(item => item.PermissionId != permission.PermissionId).ToList();

                LogSuccess(Localized.Resources.StatusItemUpdated);
                return true;
            }

            LogFailure(Localized.Resources.StatusFailedToUpdatedItem);
            return false;
        }
    }
}
