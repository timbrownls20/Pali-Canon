using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PaliCanon.Contracts.Book;

namespace PaliCanon.Data.Sql.Entities
{
    [Table("book")]
    public class BookEntity: IBookEntity 
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Nikaya { get; set; }
        public List<ChapterEntity> Chapters { get; set; }
    }
}