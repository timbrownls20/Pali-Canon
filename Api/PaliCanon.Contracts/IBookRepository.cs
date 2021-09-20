using System.Collections.Generic;
using PaliCanon.Model;

namespace PaliCanon.Contracts
{
    public interface IBookRepository
    {
        List<Book> List();
        Book Random();
        

    }
}