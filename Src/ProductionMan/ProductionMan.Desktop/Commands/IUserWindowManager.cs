using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Commands
{
    public interface IUserWindowManager
    {
        void DisplayUserEditorWindow(UserEditorWindowViewModel viewModel);

        void RequestPermissionToDelete(ConfirmDeleteWindowViewModel viewModel);

        void DisplayUserAddWindow(UserEditorWindowViewModel viewModel);
    }
}
