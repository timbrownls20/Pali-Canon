using System.Collections.Generic;

namespace PaliCanon.Data.SqlServer.Entities
{
    public class BookEntity 
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<ChapterEntity> Chapters { get; set; }
    }
}