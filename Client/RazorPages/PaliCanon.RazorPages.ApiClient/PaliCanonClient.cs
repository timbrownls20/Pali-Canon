using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaliCanon.RazorPages.ApiClient
{
    public class PaliCanonClient: IDisposable
    {
        private readonly string baseUrl;

        public HttpClient Client { get; }

        public PaliCanonClient(string baseUrl)
        {
            Client = new HttpClient();
            this.baseUrl = baseUrl;
        }

        public async Task<(List<Quote> content, HttpStatusCode status)> Search(string searchTerm)
        {
            return await Get<List<Quote>>($"{baseUrl}/search/{searchTerm}");
        }

        public async Task<(Quote content, HttpStatusCode status)> Quote()
        {
            return await Get<Quote>($"{baseUrl}/quote");
        }

        private async Task<(T content, HttpStatusCode status)> Get<T>(string api)
        {
            var response = await Client.GetAsync(api);
            return await Send<T>(response);
        }

        private async Task<(T content, HttpStatusCode status)> Post<T>(string api)
        {
            var response = await Client.GetAsync(api);
            return await Send<T>(response);
        }

        private async Task<(T content, HttpStatusCode status)> Send<T>(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            T contentObjects = JsonConvert.DeserializeObject<T>(content);
            return (contentObjects, response.StatusCode);
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}