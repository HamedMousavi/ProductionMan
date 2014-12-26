using System.Reflection;
using System.Windows.Input;
using ProductionMan.Common;

namespace ProductionMan.Desktop.Controls.MainParts
{

    public class AboutPageViewModel : NotifyPropertyChanged
    {

        public string ApplicationVersion
        {
            get
            {
                return string.Format("({0})", Assembly.GetExecutingAssembly().GetName().Version);
            }
        }


        public ICommand OpenUrlCommand { get; set; }
    }
}
