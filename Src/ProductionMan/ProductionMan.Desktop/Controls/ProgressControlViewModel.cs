using ProductionMan.Common;


namespace ProductionMan.Desktop.Controls
{

    public class ProgressControlViewModel : NotifyPropertyChanged
    {

        private string _message;


        public string Message
        {
            get { return _message; }
            set
            {
                _message = value; 
                FirePropertyChanged(this, "Message");
            }
        }
    }
}
