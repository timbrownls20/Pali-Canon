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
    public class SearchTests
    {
        [TestMethod]
        [DataRow("buddha")]
        [DataRow("water")]
        public async Task Search(string searchTerm)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (quote, status) = await client.Get<List<Quote>>($"{config.Api}search/{searchTerm}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(quote);
            Assert.IsTrue(quote.Any());
        }

        [TestMethod]
        [DataRow("buddha", 5, 1)]
        [DataRow("the", 5, 2)]
        public async Task SearchPaged(string searchTerm, int pageSize, int page)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (quote, status) = await client.Get<List<Quote>>($"{config.Api}search/{searchTerm}/{pageSize}/{page}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(quote);
            Assert.IsTrue(quote.Count() == pageSize);
        }
    }
}