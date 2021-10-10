using System.ComponentModel.DataAnnotations.Schema;
using PaliCanon.Contracts.Verse;

namespace PaliCanon.Data.Sql.Entities
{
    [Table("verse")]
    public class VerseEntity: IVerseEntity
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public ChapterEntity Chapter { get; set; }
        public int VerseNumber {get; set; }
        public int? VerseNumberLast {get; set; } 
        public string Text { get; set; }
    }

}