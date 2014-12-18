using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;


namespace ProductionMan.Domain.WebServices
{

    public class WebServiceBase
    {

        private readonly Uri _baseAddress = new Uri("http://ipv4.fiddler/ProductionMan/api/");
        //private readonly Uri _baseAddress = new Uri("http://localhost/ProductionMan/api/");
        private readonly MediaTypeWithQualityHeaderValue _mediaType = new MediaTypeWithQualityHeaderValue("application/json");
        private readonly StringWithQualityHeaderValue _defaultCharset = new StringWithQualityHeaderValue("utf-8");

        private const string ApiVersionString = "1.0";
        private const string VersionHeaderName = "Accept-Version";
        private const string EnglishLanguageName = "en-US";

        private List<StringWithQualityHeaderValue> _languages;
        protected AuthenticationHeaderValue Credentials;


        //public WebServiceBase()
        //{
        //}


        public void SetCredentials(string username, string password)
        {
            Credentials = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password))));

            // UNDONE: UPON APP LANGUAGE CHANGE, REGENERATE THIS
            SetupLanguages();
        }


        private void SetupLanguages()
        {
            _languages = new List<StringWithQualityHeaderValue>
            {
                new StringWithQualityHeaderValue(Thread.CurrentThread.CurrentUICulture.Name)
            };

            if (!string.Equals(Thread.CurrentThread.CurrentUICulture.Name, EnglishLanguageName,
                StringComparison.InvariantCultureIgnoreCase))
            {
                _languages.Add(new StringWithQualityHeaderValue(EnglishLanguageName));
            }
        }


        protected void PrepareHeaders(HttpClient client, bool setCredentials)
        {
            client.BaseAddress = _baseAddress;

            // Media type
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(_mediaType);

            // Charset
            client.DefaultRequestHeaders.AcceptCharset.Clear();
            client.DefaultRequestHeaders.AcceptCharset.Add(_defaultCharset);

            // Languages
            client.DefaultRequestHeaders.AcceptLanguage.Clear();
            foreach (var language in _languages)
            {
                client.DefaultRequestHeaders.AcceptLanguage.Add(language);
            }

            // Version
            client.DefaultRequestHeaders.Add(VersionHeaderName, ApiVersionString);

            // Credentials
            if (setCredentials)
            {
                client.DefaultRequestHeaders.Authorization = Credentials;
            }
        }


        public string ApiVersion 
        {
            get { return ApiVersionString; }
        }
    }
}
