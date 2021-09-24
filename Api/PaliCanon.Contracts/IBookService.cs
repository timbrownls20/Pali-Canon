using System.Collections.Generic;
using PaliCanon.Model;

namespace PaliCanon.Contracts
{
    public interface IBookService: IService<Book>
    {
        List<Book> List();
        Book Random();
       
    }
}