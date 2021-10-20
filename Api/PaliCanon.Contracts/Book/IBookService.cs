using System.Collections.Generic;

namespace PaliCanon.Contracts.Book
{
    public interface IBookService: IService<Model.Book>
    {
        Model.Book Get(string bookCode);
        List<Model.Book> List();
    }
}