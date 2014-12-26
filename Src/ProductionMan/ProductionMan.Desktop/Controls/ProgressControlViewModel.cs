using System.Windows.Input;
using ProductionMan.Common;

namespace ProductionMan.Desktop.Controls
{

    public class ProgressControlViewModel : NotifyPropertyChanged
    {

        private string _message;
        private ICommand _exitCommand;


        public string Message
        {
            get { return _message; }
            set
            {
                _message = value; 
                FirePropertyChanged(this, "Message");
            }
        }

        public ICommand ExitCommand
        {
            get { return _exitCommand; }
            set
            {
                _exitCommand = value;
                FirePropertyChanged(this, "ExitCommand");
            }
        }
    }
}
