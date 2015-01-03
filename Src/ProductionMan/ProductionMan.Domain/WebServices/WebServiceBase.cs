using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;


namespace ProductionMan.Domain.WebServices
{

    public class WebServiceBase
    {

        //private readonly Uri _baseAddress = new Uri("http://ipv4.fiddler/ProductionMan/api/");
        private readonly Uri _baseAddress = new Uri("http://localhost/ProductionMan/api/");
        private readonly MediaTypeWithQualityHeaderValue _mediaType = new MediaTypeWithQualityHeaderValue("application/json");
        private readonly StringWithQualityHeaderValue _defaultCharset = new StringWithQualityHeaderValue("utf-8");
        private const string ApiVersionString = "1.0";
        private const string VersionHeaderName = "Accept-Version";
        private const string EnglishLanguageName = "en-US";
        private List<StringWithQualityHeaderValue> _languages;


        public IServiceCredentialProvider ServiceCredentialProvider { get; set; }


        public Uri BaseUri
        {
            get { return _baseAddress; }
        }


        public WebServiceBase()
        {
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


        protected HttpClient CreateClient()
        {
            var client = new HttpClient {BaseAddress = _baseAddress};

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
            if (ServiceCredentialProvider != null)
            {
                ServiceCredentialProvider.AttachCredentials(client);
            }

            return client;
        }


        protected async Task<ServiceCallResult<T>> RequestGet<T>(string url)
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync(url);
                return ReadAndMap<T>(response);
            }
        }


        protected async Task<ServiceCallResult<TOut>> RequestCreate<TOut, TIn>(string url, TIn tIn)
        {
            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(url, tIn);
                return ReadAndMap<TOut>(response);
            }
        }


        protected async Task<ServiceCallResult<T>> RequestCreate<T>(string url, T t)
        {
            using (var client = CreateClient())
            {
                var response = await client.PostAsJsonAsync(url, t);
                return ReadAndMap<T>(response);
            }
        }


        protected async Task<bool> RequestDelete(string url)
        {
            using (var client = CreateClient())
            {
                var response = await client.DeleteAsync(url);
                return Map(response);
            }
        }


        protected async Task<bool> RequestUpdate<T>(string url, T item)
        {
            using (var client = CreateClient())
            {
                var response = await client.PutAsJsonAsync(url, item);
                return Map(response);
            }
        }


        private ServiceCallResult<T> ReadAndMap<T>(HttpResponseMessage response)
        {
            var result = new ServiceCallResult<T>
            {
                CallStatusCode = response.StatusCode,
                CallStatusMessage = response.ReasonPhrase
            };

            if (response.IsSuccessStatusCode)
            {
                result.Results = response.Content.ReadAsAsync<T>().Result;
            }

            return result;
        }


        private bool Map(HttpResponseMessage response)
        {
            var result = new ServiceCallResult<bool>
            {
                CallStatusCode = response.StatusCode,
                CallStatusMessage = response.ReasonPhrase,
                Results = response.IsSuccessStatusCode
            };

            return result.Results;
        }


        public string ApiVersion 
        {
            get { return ApiVersionString; }
        }
    }
}
