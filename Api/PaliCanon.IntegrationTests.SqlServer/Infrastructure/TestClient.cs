using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PaliCanon.IntegrationTests.Sql.Infrastructure
{
    class TestClient
    {
        internal HttpClient Client { get; }

        internal TestClient()
        {
            Client = new HttpClient();
        }

        internal async Task<(T content, HttpStatusCode status)> Get<T>(string api)
        {
            var response = await Client.GetAsync(api);
            return await Send<T>(response);
        }

        internal async Task<(T content, HttpStatusCode status)> Post<T>(string api)
        {
            var response = await Client.GetAsync(api);
            return await Send<T>(response);
        }

        private static async Task<(T content, HttpStatusCode status)> Send<T>(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            T contentObjects = JsonConvert.DeserializeObject<T>(content);
            return (contentObjects, response.StatusCode);
        }
    }
}
