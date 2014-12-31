using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Commands
{
    public interface IUserWindowManager
    {
        void DisplayUserEditWindow(UserWrite user);

        void RequestPermissionToDelete(UserRead user);

        void DisplayUserAddWindow(UserWrite user);

        void RequestPermissionToDelete(UserRole role);
    }
}
