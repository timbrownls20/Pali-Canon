using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaliCanon.Contracts.Book;
using PaliCanon.Data.Sql.Entities;
using PaliCanon.IntegrationTests.Sql.Infrastructure;

namespace PaliCanon.IntegrationTests.Sql.Tests
{

    [TestClass]
    public class AdminTests
    {

        [TestMethod]
        public async Task CanConnect()
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (canconnect, status) = await client.Get<bool>($"{config.Api}admin/canconnect");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(canconnect);
            Assert.IsTrue(canconnect);
        }

        [TestMethod]
        public async Task LoadBook()
        {
            const string bookCode = "dhp";
            const int numberOfChapters = 26;

            //.. arrange
            var serviceProvider = new TestServiceProvider().GetServiceProvider();
            var repo = serviceProvider.GetService<IBookRepository<BookEntity>>();

            //.. remove book before starting
            if (repo == null) Assert.Fail();
            repo.Delete(bookCode);

            var client = new TestClient();
            var config = new TestConfig();

            var (preLoadBooks, _) = await client.Get<List<BookEntity>>($"{config.Api}book/list");
            Assert.IsTrue(!preLoadBooks.Any(), $"{bookCode} not removed");

            //act
            var response = await client.Client.GetAsync($"{config.Api}admin/load/{bookCode}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var (postLoadBook, _) = await client.Get<Model.Book>($"{config.Api}book/find/{bookCode}");
            Assert.IsTrue(postLoadBook != null, $"{bookCode} not added");
            Assert.IsTrue(postLoadBook.Chapters?.Count == numberOfChapters, $"{bookCode} must have {numberOfChapters} chapters");
        }

    }
}