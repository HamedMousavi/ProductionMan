using ProductionMan.Common;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.Globalization;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class SettingsPageViewModel : NotifyPropertyChanged
    {
        private readonly ILanguageService _languageService;
        private ObservableCollection<Language> _languages;
        private Language _selectedLanguage;


        public SettingsPageViewModel(ILanguageService languageService)
        {
            Languages = new ObservableCollection<Language>();

            _languageService = languageService;
            _languageService.PropertyChanged += LanguageServiceOnPropertyChanged;
            PropertyChanged += OnPropertyChanged;

            ReloadLanguages();
            ReloadSelectedLanguage();
        }


        private void LanguageServiceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("CurrentLanguage"))
            {
                ReloadSelectedLanguage();
            }
            else if (e.NameIs("Languages"))
            {
                ReloadLanguages();
            }
        }


        private void ReloadSelectedLanguage()
        {
            SelectedLanguage = _languageService.CurrentLanguage;
        }


        private void ReloadLanguages()
        {
            Languages.Clear();
            foreach (var language in _languageService.Languages)
            {
                Languages.Add(language);
            }
        }


        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("SelectedLanguage"))
            {
                _languageService.CurrentLanguage = SelectedLanguage;
            }
        }


        public ObservableCollection<Language> Languages
        {
            get { return _languages; }
            private set
            {
                _languages = value; 
                FirePropertyChanged(this, "Languages");
            }
        }


        public Language SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    FirePropertyChanged(this, "SelectedLanguage");
                }
            }
        }
    }
}
