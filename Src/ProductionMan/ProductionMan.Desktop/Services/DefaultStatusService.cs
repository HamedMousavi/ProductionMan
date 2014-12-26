using System.ComponentModel;
using ProductionMan.Common;

namespace ProductionMan.Desktop.Services
{

    public class DefaultStatusService : NotifyPropertyChanged, IStatusService
    {

        public DefaultStatusService(ILanguageService languageService)
        {
            Status = new Status();
            languageService.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.NameIs("CurrentLanguage"))
                {
                    SetStatus(Status.Levels.Success, Localized.Resources.StatusLanguageChanged);
                }
            };
        }


        public void SetStatus(Status.Levels level, string message)
        {
            Status.Level = level;
            Status.Message = message;
            FirePropertyChanged(this, "Status");
        }


        public Status Status { get; private set; }
    }
}
