using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ProductionMan.Common;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Desktop
{

    public class MainViewModel : NotifyPropertyChanged
    {

        private List<User> _users;


        public MainViewModel()
        {
            LoadUsersAsync();
        }


        public List<User> Users
        {
            get { return _users ?? CreateUsers(); }
            private set { _users = value; FirePropertyChanged(this, "Users"); }
        }


        private static List<User> CreateUsers()
        {
            return new List<User> {new User {Name = "Loading list, please wait..."}};
        }


        public async void LoadUsersAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/ProductionMan/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "1", "1"))));

                // HTTP GET
                var response = await client.GetAsync("api/Users");
                if (response.IsSuccessStatusCode)
                {
                    Users = response.Content.ReadAsAsync<List<User>>().Result;
                }
            }
        }
    }
}
