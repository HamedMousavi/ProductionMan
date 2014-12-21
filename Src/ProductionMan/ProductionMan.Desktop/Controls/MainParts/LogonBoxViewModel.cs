using ProductionMan.Common;
using ProductionMan.Domain.Security;

namespace ProductionMan.Desktop.Controls.MainParts
{
    public class LogonBoxViewModel : NotifyPropertyChanged
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                FirePropertyChanged(this, "User");
            }
        }
    }
}
