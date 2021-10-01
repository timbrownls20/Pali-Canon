using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (quote, status) = await client.Get<Quote>($"{config.Api}quote");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(quote);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(quote.Text));
        }    
    }
}