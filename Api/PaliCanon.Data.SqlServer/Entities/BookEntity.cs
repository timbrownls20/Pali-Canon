using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaliCanon.Data.SqlServer.Entities
{
    [Table("Book")]
    public class BookEntity 
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Nikaya { get; set; }
        public List<ChapterEntity> Chapters { get; set; }
    }
}