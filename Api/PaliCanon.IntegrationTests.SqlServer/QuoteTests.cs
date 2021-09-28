using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace PaliCanon.IntegrationTests.SqlServer
{
    [TestClass]
    public class QuoteTests
    {
        public const string ApiRoot = "http://localhost:65006/api/";

        [TestMethod]
        public async Task GetQuote()
        {
            //.. arrange
            HttpClient client = new HttpClient();
            
            //.. act
            var response = await client.GetAsync($"{ApiRoot}quote");
            string content = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(content);
            var quote = jsonResponse["text"];

            //..assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.IsNotNull(quote);
        }    
    }
}