using System;
using System.Collections.Generic;
using System.Windows.Media;
using ProductionMan.Desktop.Localized;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.AppStatus;
using ProductionMan.Domain.Globalization;


namespace ProductionMan.Desktop.Factories
{

    public class AppServicesFactory
    {

        private ILanguageService _languageService;
        private IStatusService _statusService;


        internal IStatusService CreateStatusService()
        {
            if (_statusService == null)
            {
                _statusService = new DefaultStatusService();
            }

            if (_languageService != null)
            {
                _statusService.Register(_languageService);
            }

            return _statusService;
        }


        public ILanguageService CreateLanguageService(List<Language> languages)
        {
            return _languageService ?? (_languageService = new DefaultLanguageService {Languages = languages});
        }


        public ILanguageService CreateLanguageService()
        {
            return _languageService;
        }

        public FontFamily CreateFontService()
        {
            const string path = "pack://application:,,,/Theme/Dark/Fonts/";
            return new FontFamily(new Uri(path), Resources.AppFont);
        }
    }
}
