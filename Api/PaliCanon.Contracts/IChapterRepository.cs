using System.Collections.Generic;

namespace PaliCanon.Contracts
{
    public interface IChapterRepository<T>: IRepository<T> where T : class
    {
        List<T> Get(string bookCode, int? chapter, int? verse);
        T Quote(string bookCode);
        T Next(string bookCode, int chapter, int verse);
        T First(string bookCode);
        T Last(string bookCode);

    }
}