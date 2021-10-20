using System.Collections.Generic;

namespace PaliCanon.Contracts.Book
{
    public interface IBookRepository<T>: IRepository<T> where T : class, IBookEntity
    {
        T Get(string bookCode);
        List<T> List();
        void Delete(string bookCode);

    }
}