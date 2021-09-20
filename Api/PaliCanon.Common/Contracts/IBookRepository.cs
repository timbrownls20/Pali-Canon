using System.Collections.Generic;
using PaliCanon.Model;

namespace PaliCanon.Common.Contracts
{
    public interface IBookRepository
    {
        List<Book> List();
        Book Random();
        

    }
}