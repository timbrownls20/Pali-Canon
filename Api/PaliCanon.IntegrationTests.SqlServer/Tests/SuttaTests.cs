using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaliCanon.IntegrationTests.Sql.Infrastructure;
using PaliCanon.Model;

namespace PaliCanon.IntegrationTests.Sql.Tests
{
    [TestClass]
    public class SuttaTests
    {
        [TestMethod]
        [DataRow("dhp", 1, 20, "Pairs")]
        [DataRow("dhp", 5, 16, "The Fool")]
        public async Task GetChapter(string bookCode, int chapterNumber, int expectedVerseNumber, string expectedTitle)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            var (chapter, status) = await client.Get<Chapter>($"{config.Api}sutta/{bookCode}/{chapterNumber}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(chapter);
            Assert.IsTrue(chapter.ChapterNumber == chapterNumber);
            Assert.IsTrue(chapter.Verses.Count == expectedVerseNumber);
            Assert.IsTrue(chapter.Title.Contains(expectedTitle));
        }

        [TestMethod]
        [DataRow("dhp", 1, 20, "Pairs", 20)]
        [DataRow("dhp", 5, 10, "The Fool", 69)]
        public async Task GetVerse(string bookCode, int chapterNumber, int versePosition, string expectedTitle, int expectedVerseNumber)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            (Chapter chapter, HttpStatusCode status) = await client.Get<Chapter>($"{config.Api}sutta/{bookCode}/{chapterNumber}/{versePosition}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(chapter);
            Assert.IsTrue(chapter.ChapterNumber == chapterNumber);
            Assert.IsTrue(chapter.Verses.Count == 1);
            Assert.IsTrue(chapter.Verses.First().VerseNumber == expectedVerseNumber);
            Assert.IsTrue(chapter.Title.Contains(expectedTitle));
        }

        [TestMethod]
        [DataRow("dhp", "Pairs")]
        public async Task First(string bookCode, string expectedTitle)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            (Chapter chapter, HttpStatusCode status) = await client.Get<Chapter>($"{config.Api}sutta/first/{bookCode}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(chapter);
            Assert.IsTrue(chapter.Verses.Count == 1);
            Assert.IsTrue(chapter.Verses.First().VerseNumber == 1);
            Assert.IsTrue(chapter.Title.Contains(expectedTitle));
        }

        [TestMethod]
        [DataRow("dhp", "Holy Man", 423)]
        public async Task Last(string bookCode, string expectedTitle, int expectedVerseNumber)
        {
            //.. arrange
            var client = new TestClient();
            var config = new TestConfig();

            //.. act
            (Chapter chapter, HttpStatusCode status) = await client.Get<Chapter>($"{config.Api}sutta/last/{bookCode}");

            //..assert
            Assert.AreEqual(status, HttpStatusCode.OK);
            Assert.IsNotNull(chapter);
            Assert.IsTrue(chapter.Verses.Count == 1);
            Assert.IsTrue(chapter.Verses.First().VerseNumber == expectedVerseNumber);
            Assert.IsTrue(chapter.Title.Contains(expectedTitle));
        }
    }
}