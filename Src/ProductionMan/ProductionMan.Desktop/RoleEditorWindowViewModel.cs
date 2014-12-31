using System.Windows.Input;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Desktop
{

    public class RoleEditorWindowViewModel : BaseViewModel
    {

        private ICommand _cancelCommand;
        private ICommand _saveCommand;
        private UserRole _role;


        public UserRole Role
        {
            get { return _role; }
            set
            {
                _role = value;
                FirePropertyChanged(this, "Role");
            }
        }


        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set
            {
                _saveCommand = value;
                FirePropertyChanged(this, "SaveCommand");
            }
        }


        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                _cancelCommand = value;
                FirePropertyChanged(this, "CancelCommand");
            }
        }
    }
}
