using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaliCanon.IntegrationTests.Sql.Infrastructure;
using PaliCanon.Model;

namespace PaliCanon.IntegrationTests.Sql.Tests
{
    [TestClass]
    public class QuoteTests
    {
        [TestMethod]
        [DataRow("dhp")]
        public async Task GetQuote(string bookCode)
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