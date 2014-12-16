using ProductionMan.Common;


namespace ProductionMan.Desktop
{
    public class BaseViewModel : NotifyPropertyChanged
    {
        public string FlowDirection
        {
            get { return Localized.Resources.AppDir; }
        }
    }
}
