using System.Collections.Generic;
using System.Linq;
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
            Assert.IsTrue(!string.IsNullOrWhiteSpace(quote.Source));
        }

        [TestMethod]
        [DataRow(90)]
        [DataRow(255)]
        public async Task GetQuoteOfMaxLength(int maxLength)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (quote, status) = await client.Get<Quote>($"{config.Api}quote\\{maxLength}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(quote);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(quote.Text));
            Assert.IsTrue(quote.Text.Length <= maxLength);
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

        [TestMethod]
        [DataRow(5, 100)]
        [DataRow(5, 200)]
        public async Task GetQuotesOfMaxLength(int quoteNumber, int maxLength)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (quotes, status) = await client.Get<List<Quote>>($"{config.Api}quotes/{quoteNumber}/{maxLength}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(quotes);
            Assert.IsTrue(quotes.Count <= quoteNumber);
            Assert.IsTrue(!quotes.Any(x => x.Text.Length >= maxLength));
        }
    }
}