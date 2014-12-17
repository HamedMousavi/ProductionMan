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

        //private readonly Uri _baseAddress = new Uri("http://ipv4.fiddler/ProductionMan/api/");
        private readonly Uri _baseAddress = new Uri("http://localhost/ProductionMan/api/");
        private readonly MediaTypeWithQualityHeaderValue _mediaType = new MediaTypeWithQualityHeaderValue("application/json");
        protected AuthenticationHeaderValue Credentials;
        private List<StringWithQualityHeaderValue> _languages;
        private const string EnglishLanguageName = "en-US";


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

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(_mediaType);

            client.DefaultRequestHeaders.AcceptCharset.Clear();
            client.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));

            client.DefaultRequestHeaders.AcceptLanguage.Clear();
            foreach (var language in _languages)
            {
                client.DefaultRequestHeaders.AcceptLanguage.Add(language);
            }

            if (setCredentials)
            {
                client.DefaultRequestHeaders.Authorization = Credentials;
            }
        }
    }
}
