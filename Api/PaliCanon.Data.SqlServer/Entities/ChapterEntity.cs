using System.Collections.Generic;

namespace PaliCanon.Data.SqlServer.Entities
{
    public class ChapterEntity 
    {
        public ChapterEntity()
        {
            Verses = new List<VerseEntity>();
        }

        public int Id { get; set; }

        public int BookId { get; set; }

        public BookEntity Book { get; set; }

        public string Nikaya { get; set; }

        public string Title { get; set; }

        public int ChapterNumber { get; set; }

        public string Author { get; set; }
        
        public List<VerseEntity> Verses { get; set; }
    }

}