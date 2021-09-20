using System.Collections.Generic;
using MongoDB.Bson;

namespace PaliCanon.Data.MongoDB.Entities
{
    public class ChapterEntity 
    {
        public ChapterEntity()
        {
            Verses = new List<VerseEntity>();
        }

        public ObjectId _id { get; set; }

        public string Nikaya { get; set; }

        public string Book { get; set; }

        public string BookCode { get; set; }

        public string Title { get; set; }

        public int ChapterNumber { get; set; }

        public string Author { get; set; }
        
        public List<VerseEntity> Verses { get; set; }
    }

}