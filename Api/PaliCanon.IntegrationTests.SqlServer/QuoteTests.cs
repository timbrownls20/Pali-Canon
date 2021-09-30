using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PaliCanon.IntegrationTests.Sql.Infrastructure;
using PaliCanon.Model;

namespace PaliCanon.IntegrationTests.Sql
{
    [TestClass]
    public class QuoteTests
    {
        [TestMethod]
        public async Task GetQuote()
        {
            //.. arrange
            TestClient client = new TestClient();

            //.. act
            var (quote, status) = await client.Get<Quote>($"{TestSettings.ApiRoot}quote");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(quote);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(quote.Text));
        }    
    }
}