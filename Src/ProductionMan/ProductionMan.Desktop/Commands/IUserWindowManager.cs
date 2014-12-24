using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Commands
{
    public interface IUserWindowManager
    {
        void DisplayUserEditorWindow(UserEditorWindowViewModel viewModel);

        bool RequestPermissionToDelete(UserEditorWindowViewModel viewModel);

        void DisplayUserAddWindow(UserEditorWindowViewModel viewModel);
    }
}
