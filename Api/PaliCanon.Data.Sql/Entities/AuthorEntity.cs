using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaliCanon.Data.Sql.Entities
{
    [Table("author")]
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ChapterEntity>  Chapters { get; set; }
    }
}