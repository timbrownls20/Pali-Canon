using System.Collections.Generic;
using PaliCanon.Contracts.Verse;

namespace PaliCanon.Contracts.Chapter
{
    public interface IChapterRepository<T, TU>: IRepository<T> where T : class, IChapterEntity 
                                                                where TU : class, IVerseEntity
    {
        List<T> Get(string bookCode, int? chapter, int? verse);
        (T chapter, TU verse) Quote(string bookCode);
        T Next(string bookCode, int chapter, int verse);
        T First(string bookCode);
        T Last(string bookCode);

    }
}