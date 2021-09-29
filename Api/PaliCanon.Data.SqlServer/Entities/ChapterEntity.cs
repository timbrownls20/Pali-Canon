using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PaliCanon.Contracts.Chapter;

namespace PaliCanon.Data.Sql.Entities
{
    [Table("Chapter")]
    public class ChapterEntity: IChapterEntity
    {
        public ChapterEntity()
        {
            Verses = new List<VerseEntity>();
            Book = new BookEntity();
        }

        public int Id { get; set; }
        public int BookId { get; set; }
        public BookEntity Book { get; set; }
        public string Title { get; set; }
        public int ChapterNumber { get; set; }
        public List<VerseEntity> Verses { get; set; }
        public AuthorEntity Author { get; set; }

        public string BookCode => Book?.Code;

        [MaxLength(4000)]
        public string Citation { get; set; }
    }

}