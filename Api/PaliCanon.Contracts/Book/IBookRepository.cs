using System.Collections.Generic;

namespace PaliCanon.Contracts.Book
{
    public interface IBookRepository<T>: IRepository<T> where T : class, IBookEntity
    {
        List<T> List();
        T Random();
       
    }
}