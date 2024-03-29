﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaliCanon.IntegrationTests.Sql.Infrastructure;
using PaliCanon.Model;

namespace PaliCanon.IntegrationTests.Sql.Tests
{
    [TestClass]
    public class BookTests
    {
        [TestMethod]
        [DataRow("dhp")]
        public async Task Find(string bookCode)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (book, status) = await client.Get<Book>($"{config.Api}book/find/{bookCode}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(book);
            Assert.IsTrue(book.Code == bookCode);
        }

        [TestMethod]
        public async Task List()
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (books, status) = await client.Get<List<Book>>($"{config.Api}book/list");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count == 1);

            //.. no html entities
            Regex htmlEntityRegex = new Regex("&[A-Za-z]*;");

            List<Verse> verses = books.SelectMany(x => x.Chapters).SelectMany(x => x.Verses).ToList();
            foreach(Verse verse in verses)
            {
                if (htmlEntityRegex.Match(verse.Text).Success)
                    Assert.Fail($"html entity found in {verse.Text}");
            }
        }
     }
}