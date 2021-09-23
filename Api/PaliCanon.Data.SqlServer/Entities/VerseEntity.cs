using System.ComponentModel.DataAnnotations.Schema;

namespace PaliCanon.Data.SqlServer.Entities
{
    [Table("Verse")]
    public class VerseEntity 
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public ChapterEntity Chapter { get; set; }
        public int VerseNumber {get; set; }
        public int? VerseNumberLast {get; set; } 
        public string Text { get; set; }
    }

}