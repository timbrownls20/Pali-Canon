using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaliCanon.Data.SqlServer.Entities
{
    [Table("Chapter")]
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
        public List<VerseEntity> Verses { get; set; }
        public AuthorEntity Author { get; set; }
    }

}