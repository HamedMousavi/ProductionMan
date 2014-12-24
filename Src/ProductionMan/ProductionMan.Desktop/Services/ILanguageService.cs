using ProductionMan.Domain.Globalization;
using System.Collections.Generic;
using System.ComponentModel;


namespace ProductionMan.Desktop.Services
{

    public interface ILanguageService : INotifyPropertyChanged
    {
        Language CurrentLanguage { get; set; }
       
        IEnumerable<Language> Languages { get; set; }


        void LoadDefaultLanguage(Language defaultLang, Language fallbackLang);
    }
}
