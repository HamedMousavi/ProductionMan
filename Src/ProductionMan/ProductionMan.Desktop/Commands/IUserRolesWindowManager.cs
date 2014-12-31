using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Desktop.Commands
{
    public interface IUserRolesWindowManager
    {
        void DisplayRoleAddWindow(UserRole role);

        void DisplayRoleEditWindow(UserRole role);

        void RequestPermissionToDelete(UserRole role);
    }
}
