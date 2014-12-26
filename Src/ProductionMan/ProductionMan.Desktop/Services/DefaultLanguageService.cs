using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using ProductionMan.Common;
using ProductionMan.Domain.Globalization;

namespace ProductionMan.Desktop.Services
{

    public class DefaultLanguageService : NotifyPropertyChanged, ILanguageService
    {

        private Language _currentLanguage;
        private IEnumerable<Language> _languages;


        public DefaultLanguageService()
        {
            PropertyChanged += OnPropertyChanged;
        }


        public Language CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    FirePropertyChanged(this, "CurrentLanguage");
                }
            }
        }


        public IEnumerable<Language> Languages
        {
            get { return _languages; }
            set
            {
                _languages = value; 
                FirePropertyChanged(this, "Languages");
            }
        }


        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("CurrentLanguage"))
            {
                SaveApplicationLanguage();
                ChangeApplicationLanguage();
            }
        }


        private void ChangeApplicationLanguage()
        {
            var culture = new CultureInfo(Properties.Settings.Default.AppLanguage);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            //FrameworkElement.LanguageProperty.OverrideMetadata(
            //   typeof(FrameworkElement), new FrameworkPropertyMetadata(
            //   XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }


        private void SaveApplicationLanguage()
        {
            Properties.Settings.Default.AppLanguage = CurrentLanguage.LocaleName;
            Properties.Settings.Default.Save();
            // UNDONE: SAVE SELECTED LANGUAGE VIA SERVICE
        }


        /// <summary>
        /// Loads the default language if available as an inpute language in system.
        /// First looks up settings, if not available checks available system languages.
        /// </summary>
        /// <param name="defaultLang">The default language.</param>
        /// <param name="fallbackLang">The fallback language.</param>
        public void LoadDefaultLanguage(Language defaultLang, Language fallbackLang)
        {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.AppLanguage))
            {
                var found = Find(Properties.Settings.Default.AppLanguage);
                if (found != null)
                {
                    CurrentLanguage = found;
                    return;
                }
            }

            CurrentLanguage = FindInSystem(defaultLang.LocaleName) ? defaultLang : fallbackLang;
        }


        private bool FindInSystem(string cultureName)
        {
            if (InputLanguageManager.Current.AvailableInputLanguages != null)
            {
                foreach (CultureInfo lang in InputLanguageManager.Current.AvailableInputLanguages)
                {
                    if (string.Equals(lang.Name, cultureName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private Language Find(string cultureName)
        {
            foreach (var lang in Languages)
            {
                if (string.Equals(lang.LocaleName, cultureName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return lang;
                }
            }

            return null;
        }

    }
}
