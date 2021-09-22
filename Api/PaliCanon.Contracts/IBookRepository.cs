using System.Collections.Generic;
using PaliCanon.Model;

namespace PaliCanon.Contracts
{
    public interface IBookRepository: IRepository<Book>
    {
        List<Book> List();
        Book Random();
        

    }
}