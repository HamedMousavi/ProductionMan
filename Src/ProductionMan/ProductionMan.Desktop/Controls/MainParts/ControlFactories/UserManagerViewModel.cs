using System.Windows.Input;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class UserManagerViewModel : BaseListViewModel<UserRead>
    {
        private ICommand _toggleUserEnabledStatusCommand;

        public ICommand ToggleUserEnabledStatusCommand
        {
            get { return _toggleUserEnabledStatusCommand; }
            set
            {
                _toggleUserEnabledStatusCommand = value; 
                FirePropertyChanged(this, "ToggleUserEnabledStatusCommand");
            }
        }
    }
}
