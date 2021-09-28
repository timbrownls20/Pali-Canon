using PaliCanon.Contracts.Book;

namespace PaliCanon.Data.MongoDB.Entities
{
    public class BookEntity: IBookEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

}