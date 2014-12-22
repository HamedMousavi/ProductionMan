using ProductionMan.Web.Api.Common.Models;
using System.Windows.Input;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class UserManagerViewModel : BaseListViewModel<User>
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
