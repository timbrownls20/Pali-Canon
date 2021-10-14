using System.Collections.Generic;
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

        [TestMethod]
        [DataRow(10)]
        public async Task GetQuotes(int quoteNumber)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (quote, status) = await client.Get<List<Quote>>($"{config.Api}quotes/{quoteNumber}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(quote);
            Assert.IsTrue(quote.Count == quoteNumber);
        }
    }
}