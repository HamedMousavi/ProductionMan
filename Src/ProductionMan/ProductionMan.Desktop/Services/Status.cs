using ProductionMan.Common;

namespace ProductionMan.Desktop.Services
{

    public class Status : NotifyPropertyChanged
    {

        private string _message;
        private Levels _level;


        public enum Levels
        {
            Failure,
            Success,
            Warning,
            Info
        }


        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                FirePropertyChanged(this, "Message");
            }
        }


        public Levels Level
        {
            get { return _level; }
            set
            {
                _level = value;
                FirePropertyChanged(this, "Level");
            }
        }
    }
}
