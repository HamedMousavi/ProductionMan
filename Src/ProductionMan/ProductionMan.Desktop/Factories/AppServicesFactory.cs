using System.Collections.Generic;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.Globalization;


namespace ProductionMan.Desktop.Factories
{

    public class AppServicesFactory
    {

        private ILanguageService _languageService;
        private IStatusService _statusService;


        internal IStatusService CreateStatusService()
        {
            return _statusService ?? (_statusService = new DefaultStatusService(_languageService));
        }


        public ILanguageService CreateLanguageService(List<Language> languages)
        {
            return _languageService ?? (_languageService = new DefaultLanguageService {Languages = languages});
        }


        public ILanguageService CreateLanguageService()
        {
            return _languageService;
        }
    }
}
