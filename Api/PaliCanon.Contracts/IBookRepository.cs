using System.Collections.Generic;

namespace PaliCanon.Contracts
{
    public interface IBookRepository<T>: IRepository<T> where T : class
    {
        List<T> List();
        T Random();
       
    }
}