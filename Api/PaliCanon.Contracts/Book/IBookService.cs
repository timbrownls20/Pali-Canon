using System.Collections.Generic;

namespace PaliCanon.Contracts.Book
{
    public interface IBookService: IService<Model.Book>
    {
        List<Model.Book> List();
        Model.Book Random();
       
    }
}