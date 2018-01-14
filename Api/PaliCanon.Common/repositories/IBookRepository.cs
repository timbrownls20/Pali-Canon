using System.Collections.Generic;
using PaliCanon.Common.Model;

namespace PaliCanon.Common.Repository
{
    public interface IBookRepository
    {
        List<Book> List();
        Book Random();
        

    }
}