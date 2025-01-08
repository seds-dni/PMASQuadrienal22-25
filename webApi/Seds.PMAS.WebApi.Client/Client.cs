using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;


namespace Seds.PMAS.WebApi.Client
{
    public abstract class Client
    {
        private string _token;
        public Client(string token)
        {
            this._token = token;
        }

        protected T Get<T>(string uri) where T : new()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URL_WebApi_Seds"]);
            client.DefaultRequestHeaders.Add("apiKey", _token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<T>().Result;
            }
            else
            {
                string msg = response.Content.ReadAsStringAsync().Result;
                throw new Exception(msg);
            }
        }

        protected T Post<T>(object data, string uri) where T : new()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URL_WebApi_Seds"]);
            client.DefaultRequestHeaders.Add("apiKey", _token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync(uri, data).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<T>().Result;
            }
            else
            {
                string msg = response.Content.ReadAsStringAsync().Result;
                throw new Exception(msg);
            }
        }

        protected T Put<T>(object data, string uri) where T : class
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URL_WebApi_Seds"]);
            client.DefaultRequestHeaders.Add("apiKey", _token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PutAsJsonAsync(uri, data).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<T>().Result;
            }
            else
            {
                string msg = response.Content.ReadAsStringAsync().Result;
                throw new Exception(msg);
            }
        }

        protected Uri Create(object data, string uri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URL_WebApi_Seds"]);
            client.DefaultRequestHeaders.Add("apiKey", _token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(uri, data).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Headers.Location;
            }
            else
            {
                string msg = response.Content.ReadAsStringAsync().Result;
                throw new Exception(msg);
            }
        }
    }
}
